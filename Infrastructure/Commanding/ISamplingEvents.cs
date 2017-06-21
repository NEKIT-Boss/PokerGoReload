namespace Infrastructure.Commanding
{
    public interface ISamplingEvents
    {
        void RecognitionSucceeded();

        void RecognitionFailed(string errorMessage);
    }
}