namespace Infrastructure.Commanding
{
    public interface IVoiceRecognitionEvents
    {
        void RecognitionStarted();

        void RecognitionFinished();

        void RecognitionFailed(string errorMessage);
    }
}