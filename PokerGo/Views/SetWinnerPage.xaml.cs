using Windows.UI.Xaml.Controls;
using PokerGo.ViewModels;


namespace PokerGo.Views
{
    public sealed partial class SetWinnerPage : Page
    {
        public SetWinnerPage()
        {
            this.InitializeComponent();
        }

        private SetWinnerPageViewModel _viewModel;

        public SetWinnerPageViewModel ViewModel =>
            _viewModel ?? (_viewModel = (SetWinnerPageViewModel) DataContext);
    }
}
