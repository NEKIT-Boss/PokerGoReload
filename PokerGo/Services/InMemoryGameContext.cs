using System.Collections.Generic;
using System.Composition;
using Poker.Core.Game;
using Poker.Core.Players;

namespace PokerGo.Services
{
    [Shared]
    [Export(typeof(IGameContext))]
    [Export(typeof(IGameContextManager))]
    public class InMemoryGameContext : IGameContext, IGameContextManager
    {
        public GameConfiguration Configuration { get; set; }

        public IReadOnlyCollection<Player> Players { get; set; }

        public Game CurrentGame { get; set; }

        public void Restart()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        GameConfiguration IGameContext.Configuration => Configuration;

        IReadOnlyCollection<Player> IGameContext.Players => Players;

        Game IGameContext.CurrentGame => CurrentGame;
    }
}