using System.Threading.Tasks;
using Infrastructure.Commanding;
using Microsoft.AspNet.SignalR.Client;

namespace CommandingProxy
{
    public abstract class BaseCommandingHub<TSubscriber> : ICommandingHub<TSubscriber>
        where TSubscriber: class
    {
        private const string ConnectionUrl = "http://172.23.88.21:60000";

        protected HubConnection HubConnection { get; }

        protected IHubProxy HubProxy { get; }

        protected TSubscriber Subscriber { get; private set; }

        protected BaseCommandingHub(string hubName)
        {
            HubConnection = new HubConnection(ConnectionUrl);
            HubProxy = HubConnection.CreateHubProxy(hubName);
        }

        public void AddSubscriber(TSubscriber subscriber)
        {
            Subscriber = subscriber;
            MapInterface();
        }

        public async Task Connect()
        {
            await HubConnection.Start();
        }

        protected abstract void MapInterface();

        public void Dispose()
        {
            HubConnection?.Stop();
            HubConnection?.Dispose();
        }
    }
}