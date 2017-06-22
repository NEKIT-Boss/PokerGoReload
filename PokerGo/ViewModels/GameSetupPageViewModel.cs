using System.Windows.Input;
using Poker.Core.Game;
using PokerGo.Views;
using Prism.Commands;
using Prism.MEF2.Services;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace PokerGo.ViewModels
{
    public class GameSetupPageViewModel : ViewModelBase
    {
        private readonly ISessionStateService _sessionState;
        private readonly IExtendedNavigationService _navigationService;

        public GameConfiguration Configuration { get; } = GameConfiguration.DefaultAnte;

        public DelegateCommand CancelCommand { get; }

        public DelegateCommand ContinueCommand { get; }

        public GameSetupPageViewModel(ISessionStateService sessionState, IExtendedNavigationService navigationService)
        {
            _sessionState = sessionState;
            _navigationService = navigationService;

            CancelCommand = new DelegateCommand(Cancel);
            ContinueCommand = new DelegateCommand(Continue);

            Configuration.PropertyChanged += delegate { ContinueCommand.RaiseCanExecuteChanged(); };
        }

        private void Continue()
        {
            _sessionState.SessionState[nameof(GameConfiguration)] = Configuration;

            _navigationService.Navigate<TeamSetupPage>();
        }

        private void Cancel()
        {
            _navigationService.GoBack();
        }
    }
}