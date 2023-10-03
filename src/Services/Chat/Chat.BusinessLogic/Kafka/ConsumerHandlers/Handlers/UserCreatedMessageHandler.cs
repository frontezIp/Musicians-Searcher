using Chat.BusinessLogic.Exceptions;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Shared.Messages.IdentityMessages;

namespace Chat.BusinessLogic.Kafka.ConsumerHandlers.Handlers
{
    public class UserCreatedMessageHandler : IConsumerHandler<string, UserCreatedMessage>
    {
        private readonly IMessengerUserRepository _messengerUserRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserCreatedMessageHandler> _logger;

        public UserCreatedMessageHandler(IMessengerUserRepository messengerUserRepository, IMapper mapper, ILogger<UserCreatedMessageHandler> logger)
        {
            _messengerUserRepository = messengerUserRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task HandleAsync(string tk, UserCreatedMessage v)
        {
            var user = _mapper.Map<MessengerUser>(v);

            var definedUser = await _messengerUserRepository.GetByIdAsync(user.Id, false, CancellationToken.None);

            if (definedUser != null)
            {
                throw new DuplicatedMessageException(nameof(UserCreatedMessage), tk ?? "null");
            }

            _messengerUserRepository.Create(user);

            await _messengerUserRepository.SaveChangesAsync();

            _logger.LogInformation("User creation message was handled. New messenger user with {MessengerUserId} was added to the database", tk);
        }
    }
}
