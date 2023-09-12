using MediatR;
using Microsoft.Extensions.Logging;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Domain.Exceptions.NotFoundException;

namespace Musicians.Application.MediatoR.Features.Subscribers.Commands.Unsubscribe
{
    internal class UnsubscribeFromMusicianCommandHandler
        : IRequestHandler<UnsubscribeFromMusicianCommand>
    {
        private readonly IMusicianServiceHelper _musicianServiceHelper;
        private readonly ISubcribersRepository _subscriberRepository;
        private readonly ILogger<UnsubscribeFromMusicianCommandHandler> _logger;

        public UnsubscribeFromMusicianCommandHandler(IMusicianServiceHelper musicianServiceHelper,
            ISubcribersRepository subscriberRepository,
            ILogger<UnsubscribeFromMusicianCommandHandler> logger)
        {
            _musicianServiceHelper = musicianServiceHelper;
            _subscriberRepository = subscriberRepository;
            _logger = logger;
        }

        public async Task Handle(UnsubscribeFromMusicianCommand request, CancellationToken cancellationToken)
        {
            await _musicianServiceHelper.CheckIfMusicianExistsAsync(request.musicianId, cancellationToken);
            await _musicianServiceHelper.CheckIfMusicianExistsAsync(request.musicianToUnsubscribeId, cancellationToken);

            if (!await _subscriberRepository.IsAlreadySubscriberAsync(request.musicianToUnsubscribeId, request.musicianId))
                throw new MusicianNotFoundInSubscribersException(request.musicianToUnsubscribeId, request.musicianId);

            await _subscriberRepository.DeleteFromSubscribersAsync(request.musicianToUnsubscribeId, request.musicianId);

            _logger.LogInformation("Musician with {UnsubId} id successfully was unsubscribed from {MusicianId}", request.musicianId, request.musicianToUnsubscribeId);
        }
    }
}
