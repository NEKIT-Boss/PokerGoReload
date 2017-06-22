using Windows.UI.Xaml.Controls;
using PokerGo.ViewModels;


namespace PokerGo.Views
{
    public sealed partial class TeamSetupPage : Page
    {
        public TeamSetupPage()
        {
            this.InitializeComponent();
        }

        private TeamSetupPageViewModel _viewModel;

        public TeamSetupPageViewModel ViewModel =>
            _viewModel ?? (_viewModel = (TeamSetupPageViewModel) DataContext);
    }
}
