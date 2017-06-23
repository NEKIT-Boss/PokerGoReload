using Prism.MEF2.Services;
using Prism.Windows.Mvvm;

namespace PokerGo.ViewModels
{
    public class SetWinnerPageViewModel : ViewModelBase
    {
        private readonly IExtendedNavigationService _navigationService;

        public SetWinnerPageViewModel(IExtendedNavigationService navigationService)
        {
            _navigationService = navigationService;
        }


    }
}