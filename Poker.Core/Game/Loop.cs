﻿using System;
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
    public class Loop
    {
        public List<Player> Players { get; }
        public List<Player> InGamePlayers { get; }

        private GameConfiguration GameConfiguration { get; }

        public Hand NextHand()
        {
            var moved = _handsEnumerator.MoveNext();
            return moved ? _handsEnumerator.Current : null;
        }

        /// <summary>
        /// Starts the game, also could start the metrics thing here
        /// </summary>
        public Loop(IEnumerable<Player> players, GameConfiguration gameConfiguration)
        {
            GameConfiguration = gameConfiguration ?? throw new ArgumentNullException($"{nameof(gameConfiguration)} must be provided.");
            Players = new List<Player>(players ?? throw new ArgumentNullException($"{nameof(players)} is null!"));

            InGamePlayers = new List<Player>(Players);

            // Paying the players :)
            Players.ForEach(x => x.Chips = GameConfiguration.InitialChips);
            _handsEnumerator = Hands.GetEnumerator();
        }

        private PlayersSet PreparePlayers()
        {
            foreach (var player in InGamePlayers)
            {
                player.Reset();

                if (player.Chips <= GameConfiguration.AnteAmount)
                    player.GoBankrupt();
            }

            InGamePlayers.RemoveAll(x => x.IsBankrupt);
            return new PlayersSet(InGamePlayers);
        }

        private readonly IEnumerator<Hand> _handsEnumerator;
        private IEnumerable<Hand> Hands
        {
            get
            {
                // I'm almost sure, that managing the FirstBets here is right, otherwise we need to subclass 
                // hands, that will result in subclassing the game AND the hand, not the best option from the first sight
                PlayersSet table;
                while ((table = PreparePlayers()).IsPlaying)
                {
                    // There is some problem with setting the initial bet.
                    var nextHand = new Hand(this, table);
                    
                    table.ChargeAll(GameConfiguration.AnteAmount, nextHand.Pot);
                    nextHand.CurrentBet = GameConfiguration.AnteAmount;

                    yield return nextHand;
                }
            }
        }


    }
}
