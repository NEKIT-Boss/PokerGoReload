namespace Infrastructure.Commanding.HubEvents
{
    public interface ISamplingEvents
    {
        void RecognitionSucceeded();

        void RecognitionFailed(string errorMessage);
    }
}