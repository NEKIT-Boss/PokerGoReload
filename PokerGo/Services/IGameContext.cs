using System.Collections.Generic;
using Poker.Core.Game;
using Poker.Core.Players;

namespace PokerGo.Services
{
    public interface IGameContext
    {
        GameConfiguration Configuration { get; }

        IReadOnlyCollection<Player> Players { get; }

        Game CurrentGame { get; }
    }
}