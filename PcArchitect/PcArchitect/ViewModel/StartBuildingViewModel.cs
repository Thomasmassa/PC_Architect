using IComponent = PcArchitect.Interfaces.IComponent;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Model;
using PcArchitect.Views;
using PC_Architect.Model;
using System.Collections;
using System.Diagnostics;
using PcArchitect.Repository;
using PcArchitect.Services;

// DIT IS DE VIEWMODEL VOOR DE PAGINA WAAR DE CATAGORIEEN WORDEN WEERGEGEVEN

namespace PcArchitect.ViewModel
{
    [QueryProperty(nameof(BuildName), "BuildName")]
    public partial class StartBuildingViewModel : BaseViewModel
    {
        public string BuildName { get; set; }

        public ObservableCollection<IComponent> Components { get; set; }
        private readonly AddedComponentRepository _addedomponentRepository;
        private readonly LocalDatabase _database;
        private readonly BufferService _bufferService;
        private readonly RootFactory _rootF;
        private double TotalPrice;
        SavedBuild SavedBuild;

        //////////////////////////////////////////////

        //////////////////////////////////////////////


        public StartBuildingViewModel(AddedComponentRepository addedcomponentRepository, RootFactory rootF, LocalDatabase database, BufferService bufferService)
        {
            Components = new ObservableCollection<IComponent>();
            _bufferService = bufferService;
            _database = database;
            _addedomponentRepository = addedcomponentRepository;
            _rootF = rootF;
            AdditionalName = "";
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //ADD COMPONENTS
        public Task AddComponents()
        {
            return Task.Run(async () =>
            {
                var properties = typeof(Root).GetProperties();

                TotalPrice = 0;
                IsAdditionalPresetFrameEnabled = false;

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var list = (IList?)property.GetValue(_rootF.GetRoot2());
                        var lastItem = list?.Cast<IComponent>().LastOrDefault();

                        if (lastItem != null)
                        {
                            if (lastItem.Price != null)
                            {
                                lastItem.IsSelectedComponentFrameEnabled = true;
                                lastItem.IsPresetFrameEnabled = false;

                                TotalPrice += lastItem.Price ?? 0;
                                TotalPriceString = TotalPrice.ToString("C2");

                                if (lastItem is Storage storage)
                                {
                                    IsAdditionalPresetFrameEnabled = true;
                                    AdditionalName = "+ Add Additional Storage";
                                }
                            }

                            Components.Add(lastItem);
                        }
                    }
                }
            });
        }
        //ADD COMPONENTS


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //PAGENAVIGATED
        [RelayCommand]
        private async Task PageNavigated(NavigatedToEventArgs args)
        {
            try
            {
                SavedBuild = (SavedBuild)_bufferService.GetBufferedComponent(BuildName);
            }
            catch (Exception) { };


            TotalPriceString = "€0.00";

            if (SavedBuild.BuildName == null)
                Title = "Start Building";
            else
                Title = $"Edit: {SavedBuild.BuildName}";

            Components.Clear();
            await AddComponents();
        }
        //PAGENAVIGATED


        //////////////////////////////////////////////

        //////////////////////////////////////////////
        

        [RelayCommand]
        public async Task SaveBuild()
        {
            bool newBuild = false;

            if (SavedBuild == null)
            {
                SavedBuild = new SavedBuild();

                string name = "";
                var savedBuilds = _database.GetItemsAsync();
                var buildNames = savedBuilds.Result.Select(x => x.BuildName).ToList();

                while (name == "")
                { 
                    name = await Application.Current.MainPage.DisplayPromptAsync("Titel", "Voer titel in:", "Ok", "Cancel", placeholder: "Type hier...", 20);
                    if (name == "Cancel")
                        return;
                    if(buildNames.Contains(name))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Name already exists", "OK");
                    }
                    else
                        SavedBuild.BuildName = name;
                }
                newBuild = true;
            }

            var properties = typeof(Root).GetProperties();
            foreach (var property in properties)
            {
                var list = (IList?)property.GetValue(_rootF.GetRoot2());
                var Ilist = list.Cast<IComponent>().ToList();
                foreach (var item in Ilist)
                {
                    switch (item)
                    {
                        case Cpu cpu:
                            SavedBuild.CpuId = cpu.Id;
                            break;
                        case CpuCooler cpuCooler:
                            SavedBuild.CpuCoolerId = cpuCooler.Id;
                            break;
                        case Gpu gpu:
                            SavedBuild.GpuId = gpu.Id;
                            break;
                        case Motherboard motherboard:
                            SavedBuild.MotherboardId = motherboard.Id;
                            break;
                        case Memory memory:
                            SavedBuild.MemoryId = memory.Id;
                            break;
                        case Storage storage:
                            SavedBuild.StorageId = storage.Id;
                            break;
                        case Psu psu:
                            SavedBuild.PsuId = psu.Id;
                            break;
                        case Case case_:
                            SavedBuild.CaseId = case_.Id;
                            break;
                        case CaseFan caseFan:
                            SavedBuild.CaseFanId = caseFan.Id;
                            break;
                        case Os os:
                            SavedBuild.OsId = os.Id;
                            break;
                        default:
                            break;
                    }
                }
            }
            if (newBuild)
                await _database.SaveItemAsync(SavedBuild);
            else
                await _database.UpdateItemAsync(SavedBuild);
                
            Components.Clear();
            await _addedomponentRepository.ClearComponents();
            await Shell.Current.GoToAsync(nameof(MyBuildPage));
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //DELETECOMPONENT
        [RelayCommand]
        public async Task DeleteComponent(IComponent component)
        {
            if (component == null)
                return;

            bool choice = await Shell.Current.DisplayAlert("Your sure mate", component.Name, "Delete", "Cancel");
            if (choice)
            {
                TotalPrice -= component.Price ?? 0;
                TotalPriceString = TotalPrice.ToString("C2");

                await _addedomponentRepository.RemoveComponentAsync(component);
                Components.Clear();
                await AddComponents();
            }
        }
        //DELETECOMPONENT


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //GOTOPARTSLIST
        [RelayCommand]
        public async Task GoToPartsList(IComponent component)
        {
            if (component == null)
                return;

            Components.Clear();
            // Navigeer naar de PartsList pagina
            await Shell.Current.GoToAsync(nameof(PartListPage), false, new Dictionary<string, object>
            {
                {"ComponentName", component.Name }
            });
        }
        //GOTOPARTSLIST


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //BACKBUTTON
        [RelayCommand]
        public async Task BackButton()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(MainPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
        //BACKBUTTON
    }
}

