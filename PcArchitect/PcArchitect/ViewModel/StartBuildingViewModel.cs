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

/*

Deze klasse is verantwoordelijk voor het beheren van de toestand en operaties van een PC bouwproces.

Hier is een kort overzicht van de belangrijke elementen:
- BuildName: De naam van de huidige build.
- Components: Een collectie van componenten die aan de build zijn toegevoegd.
- TotalPrice: De totale prijs van de huidige build.
- Presets: Een lijst van voorgedefinieerde componenten.
- AddComponents(): Een methode die componenten toevoegt aan de build.
- PageNavigated(): Een methode die wordt aangeroepen wanneer de pagina wordt geladen.
- SaveBuild(): Een methode die de huidige build opslaat.
- GoToPartsList(): Een methode die navigeert naar de onderdelenlijst.
- DeleteComponent(): Een methode die een component uit de build verwijdert.
- BackButton(): Een methode die wordt aangeroepen wanneer de terugknop wordt ingedrukt.

De ViewModel definieert ook een lijst van preset componenten in de SetPresets methode. 
Deze presets worden gebruikt als standaard componenten wanneer er geen componenten zijn geselecteerd.

De AddComponents methode haalt componenten op uit het Root object en voegt ze toe aan de Components collectie. 
Als er geen componenten zijn geselecteerd, voegt het het overeenkomstige preset component toe.

De PageNavigated methode wordt aangeroepen wanneer de pagina wordt geladen. 
Het haalt de opgeslagen build op uit de buffer service en roept de AddComponents methode aan.

De SaveBuild methode slaat de huidige build op in de lokale database. Als de build nieuw is, vraagt het de gebruiker om een naam.

De GoToPartsList methode navigeert naar de onderdelenlijst pagina voor het geselecteerde component.

De DeleteComponent methode verwijdert een geselecteerd component uit de build en werkt de totale prijs bij.

De BackButton methode wordt aangeroepen wanneer de terugknop wordt ingedrukt. 
Het vraagt de gebruiker om de huidige build op te slaan voordat het terug navigeert naar de hoofdpagina.

*/

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
            Presets.Add(new Cpu { Name = "CPU", PresetImage = "cpu.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
            Presets.Add(new CpuCooler { Name = "CPU COOLER", PresetImage = "cpu_cooler.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
            Presets.Add(new Gpu { Name = "GPU", PresetImage = "gpu.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
            Presets.Add(new Motherboard { Name = "MOTHERBOARD", PresetImage = "motherboard.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
            Presets.Add(new Memory { Name = "MEMORY", PresetImage = "memory.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
            Presets.Add(new Storage { Name = "STORAGE", PresetImage = "ssd.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
            Presets.Add(new Psu { Name = "PSU", PresetImage = "psu.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
            Presets.Add(new CaseFan { Name = "CASE FAN", PresetImage = "case_fan.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
            Presets.Add(new Case { Name = "CASE", PresetImage = "case_tower.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
            Presets.Add(new Os { Name = "OS", PresetImage = "os.png", Id = 0, IsPresetFrameEnabled = true, IsAdditionalPresetFrameEnabled = false, IsSelectedComponentFrameEnabled = false });
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

            string buildName = SavedBuild?.BuildName ?? "";//pakt de naam van de build als die bestaat
            SavedBuild = new SavedBuild();//maakt een nieuwe build aan
            SavedBuild.BuildName = buildName ?? "";//zet de naam van de build in de nieuwe build als die bestaat

            if (SavedBuild.BuildName == "")
            {

                string name = "";
                var savedBuilds = _database.GetItemsAsync();
                var buildNames = savedBuilds.Result.Select(x => x.BuildName).ToList();

                while (name == "")
                {
                    name = await Shell.Current.DisplayPromptAsync("Titel", "Voer titel in:", "Ok", "Cancel", placeholder: "Type hier...", 20);
                    if (name == "Cancel" || name == null)
                        return;
                    if (buildNames.Contains(name))
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

                List<int> idList = new List<int>();

                if (Ilist == null) continue;
                foreach (var item in Ilist) { idList.Add(item.Id); }
                SavedBuild.GetType().GetProperty(property.PropertyType.GetGenericArguments()[0].Name).SetValue(SavedBuild, idList);
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
                    Components.Insert(index + 1, Presets.FirstOrDefault(x => x.GetType() == type));
                }
                else if (getpreset.Count == 2)
                {
                    Components.Insert(index + 1, Presets.FirstOrDefault(x => x.GetType() == type));
                    Components.Remove(getpreset[1]);
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

