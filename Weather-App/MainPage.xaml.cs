using Weather_App.ViewModels;

namespace Weather_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage(WeatherViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
