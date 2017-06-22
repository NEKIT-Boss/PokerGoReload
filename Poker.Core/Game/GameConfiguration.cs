using System.Runtime.Serialization;
using Prism.Mvvm;

namespace Poker.Core.Game
{
    public class GameConfiguration : BindableBase
    {
        private int _anteAmount;

        [DataMember]
        public int AnteAmount
        {
            get => _anteAmount;
            set => SetProperty(ref _anteAmount, value);
        }

        private int _minimalRaise;

        [DataMember]
        public int MinimalRaise
        {
            get => _minimalRaise;
            set => SetProperty(ref _minimalRaise, value);
        }

        private int _initialChips;

        [DataMember]
        public int InitialChips
        {
            get => _initialChips;
            set => SetProperty(ref _initialChips, value);
        }

        /// <summary>
        /// Actually I like this thing, but the only bad thing about this, is that if I only want to make
        /// The best one for my case after all I think
        /// </summary>
        public static GameConfiguration DefaultAnte => new GameConfiguration()
        {
            AnteAmount = 50,
            InitialChips = 500, 
            MinimalRaise = 25
        };
    }
}