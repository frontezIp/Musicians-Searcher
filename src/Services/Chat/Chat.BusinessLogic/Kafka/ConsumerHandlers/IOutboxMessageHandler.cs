using Shared.Messages.IdentityMessages;

namespace Chat.BusinessLogic.Kafka.ConsumerHandlers
{
    public interface IOutboxMessageHandler<Tk>
    {
        Task On(Tk messageId, UserCreatedMessage userCreatedMessage);
    }
}
