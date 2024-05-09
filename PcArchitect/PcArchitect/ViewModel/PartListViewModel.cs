﻿using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using IComponent = PcArchitect.Interfaces.IComponent;
using PcArchitect.Views;
using PC_Architect.Model;
using PcArchitect.Services;
using PcArchitect.Model;
using System.Collections;

// DIT IS DE VIEWMODEL VOOR DE LIJST VAN COMPONENTEN DIE WORDEN OPGEHAALD AFHANKELIJK VAN DE GESELECTEERDE CATEGORIE

namespace PcArchitect.ViewModel
{
    [QueryProperty(nameof(ComponentName), "ComponentName")]
    public partial class PartListViewModel : BaseViewModel
    {
        public string ComponentName { get; set; }
        public ObservableCollection<IComponent> Components { get; set; } = [];
        public ObservableCollection<IComponent> DisplayedItems { get; set; } = [];

        private readonly RootFactory _rootF;
        private readonly BufferService _bufferService;
        private readonly AddedComponentRepository _addedcomponentRepository;


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        public PartListViewModel(AddedComponentRepository addedcomponentRepository, BufferService bufferService, RootFactory rootF)
        {
            _addedcomponentRepository = addedcomponentRepository;
            _bufferService = bufferService;
            _rootF = rootF;

            DisplayedItems = [];
            Components = [];
        }


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //PAGE NAVIGATED METHOD
        [RelayCommand]
        async Task PageNavigated(NavigatedToEventArgs args)
        {
            string? condition1 = null;
            string? condition2 = null;
            int? condition3 = null;
            int? condition4 = null;

            switch (ComponentName)
            {
                case "CPU":
                    if (_rootF.GetRoot2().Motherboard.Count > 1)
                        condition1 = _rootF.GetRoot2().Motherboard[1].Socket.ToString(); // AM4, AM5, LGA1700
                    break;
                case "MOTHERBOARD":
                    if (_rootF.GetRoot2().Cpu.Count > 1)
                        condition1 = _rootF.GetRoot2().Cpu[1].Socket.ToString(); // AM4, AM5, LGA1700
                    if (_rootF.GetRoot2().Memory.Count > 1)
                    {
                        condition2 = _rootF.GetRoot2().Memory[1].Speed_type.ToString(); // DDR4, DDR5
                        condition3 = _rootF.GetRoot2().Memory[1].Module_size; // 4, 8, 16, 32, 64
                        condition4 = _rootF.GetRoot2().Memory[1].Module_count; // 2, 4, 8
                    }
                    break;
                case "MEMORY":
                    if (_rootF.GetRoot2().Motherboard.Count() > 1)
                    {
                        condition2 = _rootF.GetRoot2().Motherboard[1].MemoryType.ToString(); // DDR4, DDR5
                        condition3 = _rootF.GetRoot2().Motherboard[1].MemorySlots; // 2, 4, 8
                        condition4 = _rootF.GetRoot2().Motherboard[1].MaxMemory; //64, 128, 256, 512
                    }
                    break;
            }

            if (ComponentName != "")
            {
                Components.Clear();
                Title = $"{ComponentName} LIST";
                AddParts(ComponentName.Replace(" ", "").ToLower(), condition1, condition2, condition3, condition4);
            }

            await OnSearch("");
        }
        //PAGE NAVIGATED METHOD


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //ADD PARTS TO collectedParts
        private void AddParts(string ComponentName, string? condition1, string? condition2, int? condition3, int? condition4)
        {
            var properties = typeof(Root).GetProperties();

            foreach (var property in properties)
            {
                var itemType = property.PropertyType.GetGenericArguments()[0];
                string propertytype = itemType.Name.ToLower();
                if (propertytype == ComponentName)
                {
                    var list = (IList?)property.GetValue(_rootF.GetRoot1());
                    var Ilist = list.Cast<IComponent>().ToList();
                    foreach (var item in Ilist)
                    {
                        bool pass = true;
                        if (item.Price != null && item != null)
                        {
                            switch (item)
                            {
                                case Cpu cpu: // Als het item een CPU is
                                    if (condition1 != cpu.Socket && condition1 != null) // Vergelijk CPU socket met de eerste voorwaarde
                                        pass = false;
                                    break;
                                case Motherboard motherboard: // Als het item een moederbord is
                                    if (condition1 != motherboard.Socket && condition1 != null) // Vergelijk moederbord socket met de eerste voorwaarde
                                        pass = false;
                                    if (condition2 != motherboard.MemoryType && condition2 != null) // Vergelijk moederbord memory type met de tweede voorwaarde
                                        pass = false;
                                    if (condition3 * condition4 > motherboard.MaxMemory && condition3 != null) // Vergelijk totaal memory grootte met moederbord maximaal memory
                                        pass = false;
                                    if (condition4 > motherboard.MemorySlots && condition4 != null) // Vergelijk aantal memory sticks met aantal slots op moederbord
                                        pass = false;
                                    break;
                                case Memory memory: // Als het item geheugen is
                                    if (condition2 != memory.Speed_type && condition2 != null) // Vergelijk geheugen speed type met de tweede voorwaarde
                                        pass = false;
                                    if (condition3 < memory.Module_count && condition4 != null) // Vergelijk aantal memory sticks met aantal slots op moederbord
                                        pass = false;
                                    if (condition4 < memory.Module_count * memory.Module_size && condition3 != null) //vergelijk  totaal memory grootte met moederbord maximaal memory
                                        pass = false;
                                    break;
                            }
                            if (pass)
                                Components.Add(item);
                        }
                        continue;
                    }
                }
            }
        }
        //ADD PARTS TO collectedParts


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //SEARCHMETHOD
        [RelayCommand]
        async Task TextChanged(string newText)
        {
            if (string.IsNullOrEmpty(newText))
            {
                await Toast.Make("Searchbar is empty!").Show();
            }
            await OnSearch(newText);
            return;
        }
        private Task OnSearch(string searchText)
        {
            return Task.Run(() =>
            {
                DisplayedItems.Clear();
                var results = Components.Where(p => p.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                if (results.Count != 0)
                {
                    foreach (var result in results)
                    {
                        DisplayedItems.Add(result);
                    }
                }
            });
        }
        //SEARCHMETHOD


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //BACKBUTTON
        [RelayCommand]
        async Task BackButton()
        {
            DisplayedItems.Clear();
            Components.Clear();
            await Shell.Current.GoToAsync(nameof(StartBuildingPage));
        }
        //BACKBUTTON


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //SELECTEDPART
        [RelayCommand]
        async Task AddSelectedPartToRepository(IComponent selectedComponent) // aanpassen methode naam naar AddSelectedPartToRepository
        {
            bool choice = await Shell.Current.DisplayAlert("Selected Part", selectedComponent.Name, "OK", "Cancel");

            if (choice)
            {
                var collectedPart = Components.FirstOrDefault(p => p != null && p.Name == selectedComponent.Name);

                if (collectedPart != null)
                    await _addedcomponentRepository.AddComponentAsync(collectedPart);

                DisplayedItems.Clear();
                Components.Clear();
                await Shell.Current.GoToAsync(nameof(StartBuildingPage));
            }
            return;
        }
        //SELECTEDPART


        //////////////////////////////////////////////

        //////////////////////////////////////////////


        //PARTTODETAIL
        [RelayCommand]
        async Task PartToDetail(IComponent selectedItem)
        {
            if (selectedItem == null)
                return;

            _bufferService.BuffComponentForDetailPage(selectedItem.Name, selectedItem);

            await Shell.Current.GoToAsync($"{nameof(PartDetailPage)}", false, new Dictionary<string, object>
            {
                { "SelectedItem", selectedItem.Name}
            });
        }
        //PARTTODETAIL


        //////////////////////////////////////////////

        //////////////////////////////////////////////
    }
}
