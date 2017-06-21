using System.Composition;
using Infrastructure;
using Infrastructure.Commanding.HubEvents;
using Infrastructure.Commanding.HubProxies;
using Microsoft.AspNet.SignalR.Client;

namespace CommandingProxy.HubProxies
{
    /// <summary>
    /// For now it is single event handler thing.
    /// </summary>
    [Export(typeof(ICommandingHub<ISamplingEvents>))]
    public class SamplingHubProxy : BaseCommandingHub<ISamplingEvents>
    {
        [ImportingConstructor]
        public SamplingHubProxy() : base(HubNames.SamplingHub)
        {
        }

        protected override void MapInterface()
        {
            HubProxy.On(nameof(ISamplingEvents.RecognitionSucceeded), Subscriber.RecognitionSucceeded);
            HubProxy.On<string>(nameof(ISamplingEvents.RecognitionFailed), Subscriber.RecognitionFailed);
        }
    }
}