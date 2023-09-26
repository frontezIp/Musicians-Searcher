using MapsterMapper;
using Microsoft.Extensions.Logging;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Domain.Exceptions;
using Musicians.Domain.Models;
using Shared.Messages.IdentityMessages;

namespace Musicians.Infrastructure.Kafka.ConsumerHandlers.Handlers
{
    public class UserCreatedMessageHandler : IConsumerHandler<string, UserCreatedMessage>
    {
        private readonly IMusiciansRepository _musicianRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserCreatedMessageHandler> _logger;

        public UserCreatedMessageHandler(IMusiciansRepository musicianRepository, IMapper mapper, ILogger<UserCreatedMessageHandler> logger)
        {
            _musicianRepository = musicianRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task HandleAsync(string tk, UserCreatedMessage v)
        {
            var musician = _mapper.Map<Musician>(v);

            var definedMusician = await _musicianRepository.GetByIdAsync(musician.Id);

            if (definedMusician != null)
            {
                throw new DuplicatedMessageException(nameof(UserCreatedMessage), tk);
            }

            await _musicianRepository.CreateAsync(musician);

            _logger.LogInformation("User creation message was handled. New musician with {MusicianId} was added to the database", tk);
        }
    }
}
