namespace Identity.Application.Interfaces.MessageBroker.Producer
{
    public interface IEventProducer
    {
        Task ProduceAsync<Tk, Kv>(string topic, Tk key, Kv value)
            where Kv : class;
    }
}
