using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Core.Players
{
    /// <summary>
    /// I think this thing we will use for hand
    /// And the game itself will know only the full list and guys, that are eligible to play
    /// </summary>
    public class PlayersSet : IEnumerable<InGamePlayer>
    {
        public Pot Pot { get; }

        public bool IsPlaying => _players.Count > 1;

        /// <summary>
        /// Player, that will be under the gun
        /// Don't know whether this thing is intended to be public
        /// </summary>
        public InGamePlayer FirstPosition => _players.First();

        private readonly List<InGamePlayer> _players;
        public IReadOnlyCollection<InGamePlayer> Players => _players;

        public PlayersSet(IEnumerable<Player> players)
        {
            Pot = new Pot();
            _players = new List<InGamePlayer>(players
                .Select(x => new InGamePlayer(x, this)));
        }

        public void ChargeAll(int amount)
        {
            _players.ForEach(x => x.Bet(amount));
        }

        public void Leave(InGamePlayer leaver)
        {
            _players.Remove(leaver);
        }

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<InGamePlayer> GetEnumerator()
        {
            return _players.GetEnumerator();
        }
        
        #endregion

    }
}