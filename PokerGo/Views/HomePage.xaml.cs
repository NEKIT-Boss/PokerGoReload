using PokerGo.ViewModels;
using Windows.UI.Xaml.Controls;


namespace PokerGo.Views
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private HomePageViewModel _viewModel;

        public HomePageViewModel ViewModel => 
            _viewModel ?? (_viewModel = (HomePageViewModel) DataContext);
    }

    
}
