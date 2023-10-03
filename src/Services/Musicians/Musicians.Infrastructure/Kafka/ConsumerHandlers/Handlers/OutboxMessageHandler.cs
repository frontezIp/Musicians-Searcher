using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Domain.Exceptions;
using Musicians.Domain.Models;
using Shared.Messages.IdentityMessages;

namespace Musicians.Infrastructure.Kafka.ConsumerHandlers.Handlers
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

            var musicianRepository = scope.ServiceProvider.GetRequiredService<IMusiciansRepository>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<IOutboxMessageHandler<Tk>>>();

            var musician = _mapper.Map<Musician>(userCreatedMessage);
            var definedMusician = await musicianRepository.GetByIdAsync(musician.Id);

            if (definedMusician != null)
            {
                throw new DuplicatedMessageException(nameof(UserCreatedMessage), messageId?.ToString() ?? "null");
            }

            await musicianRepository.CreateAsync(musician);

            logger.LogInformation("User creation message was handled. New musician with {MusicianId} was added to the database", musician.Id);
        }
    }
}
