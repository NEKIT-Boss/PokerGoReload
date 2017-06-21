using Infrastructure;
using Infrastructure.Commanding;
using Infrastructure.Commanding.HubEvents;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace CommandingServer.Hubs
{
    [HubName(HubNames.VoiceRecognitionHub)]
    public class VoiceRecognitionHub : Hub<IVoiceRecognitionEvents>
    {
        public void StartRecognition()
        {
            Clients.Others.RecognitionStarted();
        }

        public void FinishRecognition()
        {
            Clients.Others.RecognitionFinished();
        }

        public void FailToRecognize(string errorMessage)
        {
            Clients.Others.RecognitionFailed(errorMessage);
        }
    }
}