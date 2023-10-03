using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shared.Messages.IdentityMessages;
using Chat.DataAccess.Repositories.Interfaces;
using Chat.DataAccess.Models;
using Chat.BusinessLogic.Exceptions;

namespace Chat.BusinessLogic.Kafka.ConsumerHandlers.Handlers
{
    internal class OutboxMessageHandler<Tk> : IOutboxMessageHandler<Tk>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;

        public OutboxMessageHandler(IServiceProvider serviceProvider,
            IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;   
        }

        public async Task On(Tk messageId, UserCreatedMessage userCreatedMessage)
        {
            using var scope = _serviceProvider.CreateScope();

            var handler = scope.ServiceProvider.GetRequiredService<IConsumerHandler<Tk, UserCreatedMessage>>();

            if (handler != null)
            {
                await handler.HandleAsync(messageId, userCreatedMessage);
                return;
            }

            var messengerUserRepository = scope.ServiceProvider.GetRequiredService<IMessengerUserRepository>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IOutboxMessageHandler<Tk>>>();

            var messengerUser = _mapper.Map<MessengerUser>(userCreatedMessage);
            var definedUser = await messengerUserRepository.GetByIdAsync(messengerUser.Id, false, CancellationToken.None);

            if (definedUser != null)
            {
                throw new DuplicatedMessageException(nameof(UserCreatedMessage), messageId?.ToString() ?? "null");
            }

            messengerUserRepository.Create(messengerUser);

            logger.LogInformation("User creation message was handled. New messenger user with {MessengerUserId} was added to the database", messengerUser.Id);
        }
    }
}
