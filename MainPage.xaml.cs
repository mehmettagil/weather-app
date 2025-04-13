using weather_App.ViewModels;

namespace weather_App
{
    public partial class MainPage : ContentPage
    {
        private readonly MainViewModel _viewModel;

        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
            
            // Sayfa yüklendiğinde hava durumu bilgilerini yükle
            Loaded += OnPageLoaded;
        }

        private async void OnPageLoaded(object sender, EventArgs e)
        {
            await _viewModel.LoadWeatherAsync();
        }
    }
}
