using Shared.Messages;

namespace Identity.Application.Interfaces.MessageBroker.Producer
{
    public interface ITransactionalEventProducer
    {
        Task ProduceAsync<Tv>(string topic, string key, Tv value)
            where Tv : class;
    }
}
