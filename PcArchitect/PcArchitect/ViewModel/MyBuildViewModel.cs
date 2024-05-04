using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// DEZE VIEWMODEL BEVAT DE LIJST VAN COMPONENTEN DIE DE GEBRUIKER HEEFT TOEGEVOEGD AAN ZIJN/HAAR BUILD

namespace PcArchitect.ViewModel
{
    public class MyBuildViewModel : BaseViewModel
    {
        public ObservableCollection<IComponent> BuildList { get; set; }    

        public MyBuildViewModel()
        {
            BuildList = new ObservableCollection<IComponent>();
        }
    }
}
