using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PC_Architect.Model;
using PC_Architect.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_Architect.ViewModel
{
    public partial class StartBuildViewModel : BaseViewModel
    {
        public ObservableCollection<CPU> CPUs { get; set; } = new();

        IPartsService partsService;

        public StartBuildViewModel(IPartsService partsService)
        {
            Title = "Start Building";
            this.partsService = partsService;
        }

        [ObservableProperty]
        bool isRefreshing;

        //[RelayCommand]
        //async Task GoToDetailsAsync(CPU cpu)
        //{
        //    if (cpu is null) return;

        //    await Shell.Current.GoToAsync($"{nameof(CPUPage)}", true, new Dictionary<string, object>
        //    {
        //        { "CPU", cpu }
        //    });
        //}   
    }
}
