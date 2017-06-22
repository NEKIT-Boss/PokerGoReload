using System;
using System.Threading.Tasks;

namespace Infrastructure.Commanding
{
    public interface ICommandingHub<in TSubscriber> : IDisposable where TSubscriber : class

    {
        void AddSubscriber(TSubscriber subscriber);
        Task Connect();
    }
}