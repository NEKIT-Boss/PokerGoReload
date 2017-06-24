using System;
using Prism.Mvvm;

namespace Poker.Core.Players
{
    public class InGamePlayer : BindableBase
    {
        private readonly Player _player;
        private readonly PlayersSet _table;

        public string Name => _player.Name;

        public NamedColor NamedColor => _player.Color;

        public int Chips
        {
            get => _player.Chips;
            set
            {
                _player.Chips = value;
                RaisePropertyChanged();
            }
        }

        private int _currentBet;
        public int CurrentBet
        {
            get => _currentBet;
            private set => SetProperty(ref _currentBet, value);
        }

        public InGamePlayer(Player player, PlayersSet table)
        {
            _player = player;
            _table = table;
        }

        public void Bet(int amount)
        {
            if (amount > _player.Chips) throw new InvalidOperationException($"{amount} is more than player has ({Chips})");

            Chips -= amount;
            CurrentBet += amount;
            _table.Pot.AddChips(amount);
        }

        public void Fold()
        {
            _table.Leave(this);
        }
    }
}