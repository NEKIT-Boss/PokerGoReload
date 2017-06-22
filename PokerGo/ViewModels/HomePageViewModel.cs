using System;
using System.Windows.Input;
using PokerGo.Views;
using Prism.Commands;
using Prism.MEF2.Services;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace PokerGo.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IExtendedNavigationService _navigationService;

        public HomePageViewModel(IExtendedNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void StartGame()
        {
            _navigationService.Navigate<GameSetupPage>();
        }
    }
}