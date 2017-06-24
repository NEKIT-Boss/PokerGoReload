using System.Collections.Generic;
using Poker.Core.Players;
using Prism.Mvvm;

namespace Poker.Core
{
    /// <summary>
    /// That is simplified version without side pots, need to make some investigations into it
    /// </summary>
    public class Pot : BindableBase
    {
        /// <summary>
        /// Current amount of chips in the pot
        /// </summary>
        private int _chips;
        public int Chips
        {
            get => _chips;
            private set => SetProperty(ref _chips, value);
        }

        /// <summary>
        /// Adding some chips to the pot, for now is a method because of AllInOption
        /// </summary>
        /// <param name="chips">Amount of chips to be added</param>
        public void AddChips(int chips)
        {
            // Simple, and incomplete, maybe
            Chips += chips;
        }

        public void Clear(IReadOnlyCollection<InGamePlayer> winners)
        {
            int chipsEach = Chips / winners.Count;
            foreach (var player in winners)
            {
                player.Chips += chipsEach;
            }
        }

        public void Clear(InGamePlayer winner)
        {
            winner.Chips += Chips;
        }
    }
}