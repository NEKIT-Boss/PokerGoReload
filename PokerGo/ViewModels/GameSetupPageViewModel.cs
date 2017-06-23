using Poker.Core.Game;
using PokerGo.Services;
using PokerGo.Views;
using Prism.Commands;
using Prism.MEF2.Services;
using Prism.Windows.Mvvm;

namespace PokerGo.ViewModels
{
    public class GameSetupPageViewModel : ViewModelBase
    {
        private readonly IGameContextManager _gameContextManager;
        private readonly IExtendedNavigationService _navigationService;

        public GameConfiguration Configuration { get; } = GameConfiguration.DefaultAnte;

        public DelegateCommand CancelCommand { get; }

        public DelegateCommand ContinueCommand { get; }

        public GameSetupPageViewModel(IGameContextManager gameContextManager, IExtendedNavigationService navigationService)
        {
            _gameContextManager = gameContextManager;
            _navigationService = navigationService;

            CancelCommand = new DelegateCommand(Cancel);
            ContinueCommand = new DelegateCommand(Continue);

            Configuration.PropertyChanged += delegate { ContinueCommand.RaiseCanExecuteChanged(); };
        }

        private void Continue()
        {
            _gameContextManager.Configuration = Configuration;

            _navigationService.Navigate<TeamSetupPage>();
        }

        private void Cancel()
        {
            _navigationService.GoBack();
        }
    }
}