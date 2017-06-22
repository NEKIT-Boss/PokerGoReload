using System.Collections.Generic;
using System.Collections.ObjectModel;
using Poker.Core;
using Poker.Core.Game;
using Poker.Core.Players;
using Prism.Commands;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;

namespace PokerGo.ViewModels
{
    public class TeamSetupPageViewModel : ViewModelBase
    {
        public ObservableCollection<Player> Players { get; }

        public ObservableCollection<NamedColor> AvailableColors { get; }

        public DelegateCommand<Player> RemovePlayerCommand { get; }

        public TeamSetupPageViewModel()
        {
            AvailableColors = new ObservableCollection<NamedColor>(NamedColor.PredefinedColors);

            Players = new ObservableCollection<Player>()
            {
                new Player("Player 1") { Color = AvailableColors[0] },
                new Player("Player 2") { Color = AvailableColors[1] }
            };

            RemovePlayerCommand = new DelegateCommand<Player>(RemovePlayer);
        }

        private void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }
    }
}