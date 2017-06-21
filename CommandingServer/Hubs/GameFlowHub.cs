using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Commanding;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CommandingServer.Hubs
{
    [HubName(HubNames.GameFlowHub)]
    public class GameFlowHub : Hub<IGameFlowEvents>
    {
        //public override Task OnConnected()
        //{
        //    // Here we can notify somehow the client about the connection
        //    return base.OnConnected();
        //}

        public void Check()
        {
            Clients.Others.Checked();
        }

        public void Bet(int amount)
        {
            Clients.Others.Bet(amount);
        }

        public void Call()
        {
            Clients.Others.Called();
        }

        public void RaiseTo(int amount)
        {
            Clients.Others.RaisedTo(amount);
        }

        public void RaiseBy(int amount)
        {
            Clients.Others.RaisedBy(amount);
        }

        public void Fold()
        {
            Clients.Others.Folded();
        }

        public void RaiseException(string errorMessage)
        {
            Clients.Others.ExceptionRaised(errorMessage);
        }
    }
}