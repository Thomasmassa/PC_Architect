using PC_Architect.ViewModel;

namespace PC_Architect;
public partial class PartsList : ContentPage
{
    public PartsList(PartsListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
	}
}