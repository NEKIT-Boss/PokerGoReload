using System;
using Prism.Mvvm;

namespace Poker.Core.Players
{
    public class Player : BindableBase
    {
        private NamedColor _color;
        public NamedColor Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        private string _name = "";
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _chips = -1;
        public int Chips
        {
            get => _chips;
            set => SetProperty(ref _chips, value);
        }

        private bool _hasFolded;
        public bool HasFolded
        {
            get => _hasFolded;
            private set => SetProperty(ref _hasFolded, value);
        }

        private int _currentBet;
        public int CurrentBet
        {
            get => _currentBet;
            private set => SetProperty(ref _currentBet, value);
        }

        public bool IsBankrupt => Chips == 0;

        public void Reset()
        {
            HasFolded = false;
            CurrentBet = 0;
        }

        public void GoBankrupt()
        {
            Chips = 0;
        }

        public void Bet(int amount, Pot pot)
        {
            if (amount > Chips) throw new InvalidOperationException($"{amount} is more than player has ({Chips})");

            Chips -= amount;
            CurrentBet += amount;
            pot.AddChips(amount);
        }

        public void Fold()
        {
            HasFolded = true;
            CurrentBet = 0;
        }

        public Player(string name, NamedColor color = null, int chips = 0)
        {
            Name = name;
            Color = color;
            Chips = chips;
        }
    }
}