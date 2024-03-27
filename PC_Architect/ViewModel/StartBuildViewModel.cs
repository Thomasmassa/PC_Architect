using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PC_Architect.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PC_Architect.ViewModel
{
    public partial class StartBuildViewModel : INotifyPropertyChanged
    {
        public string Image { get; set; }
        public string Name { get; set; }

        private ObservableCollection<IBindable>? _bindables;//Dit is een lijst van IBindable objecten

        public ObservableCollection<IBindable>? Bindables
        {
            get => _bindables;
            set //Deze setter wordt aangeroepen wanneer de Bindables property wordt aangepast
            {
                _bindables = value;
                OnPropertyChanged();
            }
        }

        public StartBuildViewModel()
        {
            Bindables = new ObservableCollection<IBindable>
            {
                new Cpu() { Name = "CPU", Image = "cpu.png" },
                new CpuCooler() { Name = "CPU Cooler", Image = "cpu_cooler.png" },
                new Motherboard() { Name = "Motherboard", Image = "motherboard.png" },
                new Memory() { Name = "Memory", Image = "memory.png" },
                new Gpu() { Name = "GPU", Image = "gpu.png" },
                new InternalStorage() { Name = "Storage", Image = "ssd.png" },
                new ExternalStorage() { Name = "External Storage", Image = "hdd.png" },
                new Psu() { Name = "PSU", Image = "psu.png" },
                new Case() { Name = "Case", Image = "case_tower.png" },
                new CaseFan() { Name = "Case Fan", Image = "case_fan.png" },
                new OS() { Name = "OS", Image = "os.png" }
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)//het event doet een check of de property is aangepast
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
