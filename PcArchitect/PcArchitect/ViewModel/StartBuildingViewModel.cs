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
using System;
using System.Reflection;

// DIT IS DE VIEWMODEL VOOR DE PAGINA WAAR DE CATAGORIEEN WORDEN WEERGEGEVEN

namespace PcArchitect.ViewModel
{
    [QueryProperty(nameof(BuildName), "BuildName")]
    public partial class StartBuildingViewModel : BaseViewModel
    {
        public string? BuildName { get; set; }

        public ObservableCollection<IComponent> Components { get; set; }
        private readonly AddedComponentRepository _addedomponentRepository;
        private readonly LocalDatabase _database;
        private readonly BufferService _bufferService;
        private readonly RootFactory _rootF;
        private double TotalPrice;
        SavedBuild? SavedBuild;

        private List<IComponent> Presets = [];


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

            SetPresets();
        }

        private void SetPresets()
        {
            Presets.Add(new Cpu { Name = "CPU", Image = "cpu.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false });
            Presets.Add(new CpuCooler { Name = "CPU COOLER", Image = "cpu_cooler.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false });
            Presets.Add(new Gpu { Name = "GPU", Image = "gpu.png", Id = 0, IsPresetFrameEnabled = true , IsAdditionalPresetFrameEnabled = false });
            Presets.Add(new Motherboard { Name = "MOTHERBOARD", Image = "motherboard.png", Id = 0, IsPresetFrameEnabled = true , IsAdditionalPresetFrameEnabled = false });
            Presets.Add(new Memory { Name = "MEMORY", Image = "memory.png", Id = 0, IsPresetFrameEnabled = true , IsAdditionalPresetFrameEnabled = false });
            Presets.Add(new Storage { Name = "STORAGE", Image = "ssd.png", Id = 0, IsPresetFrameEnabled = true , IsAdditionalPresetFrameEnabled = false });
            Presets.Add(new Psu { Name = "PSU", Image = "psu.png", Id = 0, IsPresetFrameEnabled = true , IsAdditionalPresetFrameEnabled = false });
            Presets.Add(new CaseFan { Name = "CASE FAN", Image = "case_fan.png", Id = 0, IsPresetFrameEnabled = true , IsAdditionalPresetFrameEnabled = false });
            Presets.Add(new Case { Name = "CASE", Image = "case_tower.png", Id = 0, IsPresetFrameEnabled = true , IsAdditionalPresetFrameEnabled = false });
            Presets.Add(new Os { Name = "OS", Image = "os.png", Id = 0, IsPresetFrameEnabled = true , IsAdditionalPresetFrameEnabled = false });
        }



        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //ADD COMPONENTS
        public Task AddComponents()
        {
            return Task.Run(() =>
            {
                var properties = typeof(Root).GetProperties();

                TotalPrice = 0;

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var list = (IList?)property.GetValue(_rootF.GetRoot2());
                        var Items = list?.Cast<IComponent>().ToList();

                        if (Items == null || Items.Count == 0)
                        {
                            var getpreset = Presets.FirstOrDefault(x => x.GetType() == property.PropertyType.GetGenericArguments()[0]);
                            Components.Add(getpreset);
                            continue;
                        }
                        
                        foreach (var item in Items)
                        {
                            item.IsSelectedComponentFrameEnabled = true;
                            item.IsPresetFrameEnabled = false;

                            TotalPrice += item.Price ?? 0;
                            TotalPriceString = TotalPrice.ToString("C2");
                            Components.Add(item);   
                        }   
                        if (list is List<Memory> && list.Count > 0)
                            Components.Add(new Memory { AdditionalName = "Memory", AdditionalDescription = "+ Add Additional Memory", IsAdditionalPresetFrameEnabled = true });
                        if (list is List<Storage> && list.Count > 0)
                            Components.Add(new Storage { AdditionalName = "Storage", AdditionalDescription = "+ Add Additional Storage", IsAdditionalPresetFrameEnabled = true });
                        if (list is List<CaseFan> && list.Count > 0)
                            Components.Add(new CaseFan { AdditionalName = "Case Fan", AdditionalDescription = "+ Add Additional Case Fan", IsAdditionalPresetFrameEnabled = true });
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
            if (BuildName != null)
                SavedBuild = (SavedBuild)_bufferService.GetBufferedComponent(BuildName);

            TotalPriceString = "€0.00";

            if (SavedBuild == null)
                Title = "Start Building";
            else
                Title = $"Edit: {SavedBuild.BuildName}";

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
                    name = await Shell.Current.DisplayPromptAsync("Titel", "Voer titel in:", "Ok", "Cancel", placeholder: "Type hier...", 20);
                    if (name == "Cancel" || name == null)
                        return;
                    if(buildNames.Contains(name))
                    {
                        await Shell.Current.DisplayAlert("Error", $"{name} already exists", "OK");
                        name = "";
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
                var Ilist = list?.Cast<IComponent>().ToList();

                if (Ilist == null) continue;
                foreach (var item in Ilist)
                {
                    switch (item)
                    {
                        case Cpu cpu:
                            SavedBuild.CpuId.Add(cpu.Id);
                            break;
                        case CpuCooler cpuCooler:
                            SavedBuild.CpuCoolerId.Add(cpuCooler.Id);
                            break;
                        case Gpu gpu:
                            SavedBuild.GpuId.Add(gpu.Id);
                            break;
                        case Motherboard motherboard:
                            SavedBuild.MotherboardId.Add(motherboard.Id);
                            break;
                        case Memory memory:
                            SavedBuild.MemoryId.Add(memory.Id);
                            break;
                        case Storage storage:
                            SavedBuild.StorageId.Add(storage.Id);
                            break;
                        case Psu psu:
                            SavedBuild.PsuId.Add(psu.Id);
                            break;
                        case Case case_:
                            SavedBuild.CaseId.Add(case_.Id);
                            break;
                        case CaseFan caseFan:
                            SavedBuild.CaseFanId.Add(caseFan.Id);
                            break;
                        case Os os:
                            SavedBuild.OsId.Add(os.Id);
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
            SavedBuild = null;
            await _addedomponentRepository.ClearComponents();
            await Shell.Current.GoToAsync(nameof(MyBuildPage));
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //GOTOPARTSLIST
        [RelayCommand]
        public async Task GoToPartsList(IComponent component)
        {
            var componentName = "";
            if (component == null)
                return;

            if (component.AdditionalName != null)
                componentName = component.AdditionalName;
            else
                componentName = component.Name;

            Components.Clear();

            if (componentName != null)
                await Shell.Current.GoToAsync(nameof(PartListPage), false, new Dictionary<string, object>
                {
                    {"ComponentName", componentName }
                });
        }
        //GOTOPARTSLIST


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


                int index = 0;
                var type = component.GetType();

                var getiteminList = Components.FirstOrDefault(x => x.GetType() == type);
                index = Components.IndexOf(getiteminList);

                var getpreset = Components.Where(x => x.GetType() == type).ToList();
                if (getpreset.Count == 1)
                {
                    Components.Insert(index, Presets.FirstOrDefault(x => x.GetType() == type));
                }
                else if (getpreset.Count == 2)
                {
                    Components.Remove(getpreset[1]);
                    Components.Insert(index, Presets.FirstOrDefault(x => x.GetType() == type));
                }

                Components.Remove(component);
            }
        }
        //DELETECOMPONENT
        


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //BACKBUTTON
        [RelayCommand]
        public async Task BackButton()
        {
            if (SavedBuild != null)
            {
                bool choice = await Shell.Current.DisplayAlert("WARNING", "Wil je opslaan voor je vertrekt", "Save", "Discard");
                if (choice)
                    await SaveBuild();
                else
                    await _addedomponentRepository.ClearComponents();

                SavedBuild = null;
            }
                Components.Clear();

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

