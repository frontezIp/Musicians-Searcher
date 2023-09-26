using Shared.Messages.IdentityMessages;

namespace Musicians.Infrastructure.Kafka.ConsumerHandlers
{
    public interface IOutboxMessageHandler<Tk>
    {
        Task On(Tk messageId, UserCreatedMessage userCreatedMessage);
    }
}
