using IComponent = PcArchitect.Interfaces.IComponent;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PcArchitect.Model;
using PcArchitect.Views;
using PC_Architect.Model;
using System.Collections;
using System.Diagnostics;
using PcArchitect.Repository;

// DIT IS DE VIEWMODEL VOOR DE PAGINA WAAR DE CATAGORIEEN WORDEN WEERGEGEVEN

namespace PcArchitect.ViewModel
{
    public partial class StartBuildingViewModel : BaseViewModel
    {
        public ObservableCollection<IComponent> Components { get; set; }
        private readonly AddedComponentRepository _addedomponentRepository;
        private readonly RootFactory _rootF;
        private double TotalPrice;
        private readonly LocalDatabase _database;
        private SavedBuild SavedBuild;

        //////////////////////////////////////////////

        //////////////////////////////////////////////


        public StartBuildingViewModel(AddedComponentRepository addedcomponentRepository, RootFactory rootF, LocalDatabase database)
        {
            Components = new ObservableCollection<IComponent>();
            _database = database;
            _addedomponentRepository = addedcomponentRepository;
            _rootF = rootF;

            TotalPriceString = "€0.00";

            Title = "";
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
                                    Title = "+ Add Additional Storage";
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
            Components.Clear();
            await AddComponents();
        }
        //PAGENAVIGATED


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


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        [RelayCommand]
        public async Task SaveBuild()
        {
            var result = await Application.Current.MainPage.DisplayPromptAsync("Titel", "Voer titel in:", "OK", "Annuleren", placeholder: "Type hier...");
            if (result != null)
            {
                var properties = typeof(Root).GetProperties();
                SavedBuild = new SavedBuild();
                SavedBuild.BuildName = result;
                foreach (var property in properties)
                {
                    var itemType = property.PropertyType.GetGenericArguments()[0];
                    string propertytype = itemType.Name.ToLower();

                    var list = (IList?)property.GetValue(_rootF.GetRoot2());
                    var Ilist = list.Cast<IComponent>().ToList();
                    foreach (var item in Ilist)
                    {
                        if (item.Price != null && item != null)
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
                }
                await _database.SaveItemAsync(SavedBuild);
            }
            Components.Clear();
            await Shell.Current.GoToAsync(nameof(MyBuildPage));
        }
    }
}

