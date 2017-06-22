using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Core.Players
{
    /// <summary>
    /// I think this thing we will use for hand
    /// And the game itself will know only the full list and guys, that are eligible to play
    /// </summary>
    public class PlayersSet : IEnumerable<Player>
    {
        public PlayersSet(IEnumerable<Player> players)
        {
            _players = new List<Player>(players);
        }

        public bool IsPlaying => _players.Count != 1;

        /// <summary>
        /// Player, that will be under the gun
        /// Don't know whether this thing is intended to be public
        /// </summary>
        public Player FirstPosition => _players.First();

        public void ChargeAll(int amount, Pot pot)
        {
            _players.ForEach(x => x.Bet(amount, pot));
        }

        private readonly List<Player> _players;
        public IReadOnlyCollection<Player> Players => _players;

        // Here is some error, we can not handle the situation, when everyone folded except one,
        // And cease the game, and actually the thing with bankrupcy is also broken for now.
        // But we could fix that later
        private void RemoveInactive()
        {
            _players.RemoveAll(x => x.HasFolded || x.IsBankrupt);
        }

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<Player> GetEnumerator()
        {
            RemoveInactive();
            return _players.GetEnumerator();
        }
        
        #endregion

    }
}