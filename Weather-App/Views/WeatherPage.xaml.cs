using Weather_App.ViewModels;

namespace Weather_App.Views
{
    public partial class WeatherPage : ContentPage
    {
        public WeatherPage(WeatherViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
} 