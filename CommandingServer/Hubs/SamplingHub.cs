using Microsoft.AspNet.SignalR;

namespace CommandingServer.Hubs
{
    public class SamplingHub : Hub
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