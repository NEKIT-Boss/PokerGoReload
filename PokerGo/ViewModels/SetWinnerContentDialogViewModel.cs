using System.Collections.Generic;
using Poker.Core.Players;
using PokerGo.Services;
using Prism.MEF2.Services;
using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;

namespace PokerGo.ViewModels
{
    public class SetWinnerContentDialogViewModel : ViewModelBase
    {
        private readonly IExtendedNavigationService _navigationService;

        public PlayersSet Table { get; }

        public SetWinnerContentDialogViewModel(IExtendedNavigationService navigationService, PlayersSet table)
        {
            _navigationService = navigationService;

            Table = table;
        }

        public void SetWinners(IReadOnlyCollection<InGamePlayer> winners)
        {
            Table.Pot.Clear(winners);
        }
    }
}