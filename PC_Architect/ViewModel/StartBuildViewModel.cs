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
            Bindables = new ObservableCollection<IBindable>
            {
                new Cpu(){ Name = "CPU", ImageSource = "cpu.png" },
                new Motherboard(){ Name = "Motherboard", ImageSource = "motherboard.png" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnpropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
