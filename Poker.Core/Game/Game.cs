using System;
using System.Collections.Generic;
using Poker.Core.Flow;
using Poker.Core.Players;

namespace Poker.Core.Game
{
    /// <summary>
    /// For now it is only Texas Hold 'Em, later
    /// We could add more modes by deriving from this
    /// Think about metrics and stuff here
    /// </summary>
    public class Game
    {
        public List<Player> Players { get; }
        public List<Player> InGamePlayers { get; }

        public GameConfiguration Configuration { get; }

        public Hand NextHand()
        {
            var moved = _handsEnumerator.MoveNext();
            return moved ? _handsEnumerator.Current : null;
        }

        /// <summary>
        /// Starts the game, also could start the metrics thing here
        /// </summary>
        public Game(IEnumerable<Player> players, GameConfiguration gameConfiguration)
        {
            Configuration = gameConfiguration ?? throw new ArgumentNullException($"{nameof(gameConfiguration)} must be provided.");
            Players = new List<Player>(players ?? throw new ArgumentNullException($"{nameof(players)} is null!"));

            InGamePlayers = new List<Player>(Players);

            Players.ForEach(x => x.Chips = Configuration.InitialChips);
            _handsEnumerator = Hands.GetEnumerator();
        }

        private PlayersSet PreparePlayers()
        {
            // TODO better implement event aggragation here, than manually remove players out of table.
            // TODO FIX BANKRUPCY PROBLEM
            InGamePlayers.RemoveAll(x => x.Chips <= Configuration.AnteAmount);
            return new PlayersSet(InGamePlayers);
        }

        private readonly IEnumerator<Hand> _handsEnumerator;
        private IEnumerable<Hand> Hands
        {
            get
            {
                PlayersSet table;
                while ((table = PreparePlayers()).IsPlaying)
                {
                    // There is some problem with setting the initial bet.
                    var nextHand = new Hand(this, table);
                    
                    table.ChargeAll(Configuration.AnteAmount);
                    nextHand.CurrentBet = Configuration.AnteAmount;

                    yield return nextHand;
                }
            }
        }


    }
}
