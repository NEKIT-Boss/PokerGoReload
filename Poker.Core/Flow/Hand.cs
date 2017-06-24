using System.Collections.Generic;
using System.Linq;
using Poker.Core.Players;
using Prism.Mvvm;

namespace Poker.Core.Flow
{
    /// <summary>
    /// Later we could subclass it, and change the behavior, but for now only for one case
    /// </summary>
    public class Hand : BindableBase
    {
        /// <summary>
        /// The game, associated with this hand
        /// </summary>
        private Game.Game CurrentGame { get; }

        /// <summary>
        /// Players, effectively returning them one by one
        /// </summary>
        public PlayersSet Table { get; }

        private int _currentBet;
        /// <summary>
        /// Exposing the current bet, holding actually the current bet.
        /// </summary>
        public int CurrentBet
        {
            get => _currentBet;
            set => SetProperty(ref _currentBet, value);
        }


        public bool IsEquallyBet => Table.Players
            .All(x => x.CurrentBet == CurrentBet);

        public BettingRound NextBettingRound()
        {
            bool moved = _bettingRoundsEnumerator.MoveNext();
            return moved ? _bettingRoundsEnumerator.Current : null;
        }

        private static readonly IReadOnlyCollection<string> RoundNames = new []
        {
            "Preflop", "Flop", "Turn", "River"
        };

        private readonly IEnumerator<BettingRound> _bettingRoundsEnumerator;
        /// <summary>
        /// All of the rounds, kind of factory actually with yields the best thing
        /// </summary>
        private IEnumerable<BettingRound> BettingRounds
        {
            get
            {
                foreach (string roundName in RoundNames)
                {
                    if (!Table.IsPlaying) yield break;

                    yield return new BettingRound(this, roundName);
                }
            }
        }

        public Hand(Game.Game game, PlayersSet table)
        {
            CurrentGame = game;
            Table = table;

            _bettingRoundsEnumerator = BettingRounds.GetEnumerator();
        }
    }
}