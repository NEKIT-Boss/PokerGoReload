using Microsoft.AspNet.SignalR;

namespace CommandingServer.Hubs
{
    public class GameFlowHub : Hub
    {
        public void Check()
        {
            Clients.Others.Check();
        }

        public void Bet(int amount)
        {
            Clients.Others.Bet(amount);
        }

        public void RaiseTo(int amount)
        {
            Clients.Others.RaiseTo(amount);
        }

        public void RaiseBy(int amount)
        {
            Clients.Others.RaiseBy(amount);
        }

        public void Fold()
        {
            Clients.Others.Fold();
        }

        public void RaiseException(string errorMessage)
        {
            Clients.Others.Exception(errorMessage);
        }
    }
}