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

        public Player(string name, NamedColor color = null, int chips = 0)
        {
            Name = name;
            Color = color;
            Chips = chips;
        }
    }
}