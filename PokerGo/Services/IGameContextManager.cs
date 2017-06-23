using System.Collections.Generic;
using Poker.Core.Game;
using Poker.Core.Players;

namespace PokerGo.Services
{
    public interface IGameContextManager
    {
        GameConfiguration Configuration { get; set; }

        IReadOnlyCollection<Player> Players { get; set; }

        Game CurrentGame { get; set; }

        void Restart();

        void Reset();
    }
}