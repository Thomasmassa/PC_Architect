using PC_Architect.Model;
using PC_Architect.Services;
using System.Collections.ObjectModel;

namespace PC_Architect.ViewModel
{
    public class PartsListViewModel : BaseViewModel
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Discription { get; set; }
        public double? Price { get; set; }


        public ObservableCollection<PartViewModel> PartViewModels { get; set; }

        public PartsListViewModel()
        {
            PartViewModels = new ObservableCollection<PartViewModel>();
            var parts = DataStore.Parts;
            AddParts(parts);
        }

        private void AddParts(List<IComponent> parts)
        {
            string details = string.Empty;
            Title = "CPU LIST";


            foreach (var part in parts)
            {
                if (part == null)
                {
                    continue;
                }

                if (part is Cpu cpu)
                {
                    details = $"Socket: {cpu.Socket}\nCores: {cpu.Core_Count}\nCore Clock: {cpu.Core_clock}\nBoost Clock: {cpu.BoostClock}";
                }
                var partViewModel = new PartViewModel
                {
                    Name = part.Name,
                    Image = part.Image,
                    Price = part.Price,
                    Discription = details
                };

                PartViewModels.Add(partViewModel);
            }
        }

        public ObservableCollection<IComponent> Parts { get; set; }
    }
}
