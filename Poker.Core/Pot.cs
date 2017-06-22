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

        /// <summary>
        /// Disposes all of the chips of the pot and also could then log it,
        /// make the records, whatever.
        /// </summary>
        /// <returns>All the chips in the pot!</returns>
        public int Clear()
        {
            int chips = Chips;
            Chips = 0;
            return chips;
        }
    }
}