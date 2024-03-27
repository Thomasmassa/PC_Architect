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
        private ObservableCollection<IBindable> _bindables;
        private IBindable _selectedBindable;

        public ObservableCollection<IBindable> Bindables
        {
            get => _bindables;
            set 
            {
                _bindables = value;
                OnpropertyChanged();
            }
        }
        public IBindable SelectedBindable
        {
            get => _selectedBindable;
            set
            {
                _selectedBindable = value;
                OnpropertyChanged();
            }
        }

        public StartBuildViewModel()
        {
            Bindables =
            [
                new Cpu(){ Name = "CPU", ImageSource = "cpu.png" },
                new CpuCooler(){ Name = "CPU Cooler", ImageSource = "cpu_cooler.png" },
                new Motherboard(){ Name = "Motherboard", ImageSource = "motherboard.png" },
                new Memory(){ Name = "Memory", ImageSource = "memory.png" },
                new Gpu(){ Name = "GPU", ImageSource = "gpu.png" },
                new InternalStorage(){ Name = "Storage", ImageSource = "ssd.png" },
                new ExternalStorage(){ Name = "External Storage", ImageSource = "hdd.png" },
                new Psu(){ Name = "PSU", ImageSource = "psu.png" },
                new Case(){ Name = "Case", ImageSource = "case_tower.png" },
                new CaseFan(){ Name = "Case Fan", ImageSource = "case_fan.png" },
                new OS(){ Name = "OS", ImageSource = "os.png" }
            ];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnpropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
