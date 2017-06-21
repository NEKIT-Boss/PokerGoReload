namespace Infrastructure.Commanding.HubEvents
{
    public interface IGameFlowEvents
    {
        void Checked();

        void Bet(int amount);

        void Called();

        void RaisedBy(int amount);

        void RaisedTo(int amount);

        void Folded();

        void ExceptionRaised(string errorMessage);
    }
}