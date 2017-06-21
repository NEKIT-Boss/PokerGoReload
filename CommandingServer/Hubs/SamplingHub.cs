using Infrastructure;
using Infrastructure.Commanding;
using Infrastructure.Commanding.HubEvents;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CommandingServer.Hubs
{
    [HubName(HubNames.SamplingHub)]
    public class SamplingHub : Hub<ISamplingEvents>
    {
        public void RecognizeSample()
        {
            Clients.Others.RecognitionSucceeded();
        }

        public void FailToRecognizeSample(string errorMessage)
        {
            Clients.Others.RecognitionFailed(errorMessage);
        }
    }
}