using PC_Architect.Services;
using PC_Architect.ViewModel;

namespace PC_Architect;

public partial class startBuilding : ContentPage
{
    public startBuilding(StartBuildViewModel startBuild)
    {
        InitializeComponent();
        BindingContext = startBuild;
    }
}