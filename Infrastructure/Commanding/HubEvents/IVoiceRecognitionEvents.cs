namespace Infrastructure.Commanding.HubEvents
{
    public interface IVoiceRecognitionEvents
    {
        void RecognitionStarted();

        void RecognitionFinished();

        void RecognitionFailed(string errorMessage);
    }
}