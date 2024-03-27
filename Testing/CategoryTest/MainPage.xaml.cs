using CategoryTest.Service;
using CategoryTest.ViewModel;

namespace CategoryTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new CategoryViewModel(new MockCategorieService());
        }
    }
}
