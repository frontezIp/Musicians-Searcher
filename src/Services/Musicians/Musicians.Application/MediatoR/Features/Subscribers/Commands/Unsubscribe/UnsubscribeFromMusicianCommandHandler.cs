﻿using MediatR;
using Microsoft.Extensions.Logging;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Domain.Exceptions.NotFoundException;

namespace Musicians.Application.MediatoR.Features.Subscribers.Commands.Unsubscribe
{
    internal class UnsubscribeFromMusicianCommandHandler
        : IRequestHandler<UnsubscribeFromMusicianCommand>
    {
        private readonly IMusicianServiceHelper _musicianServiceValidator;
        private readonly ISubcribersRepository _subscriberRepository;
        private readonly ILogger<UnsubscribeFromMusicianCommandHandler> _logger;

        public UnsubscribeFromMusicianCommandHandler(IMusicianServiceHelper musicianServiceValidator,
            ISubcribersRepository subscriberRepository,
            ILogger<UnsubscribeFromMusicianCommandHandler> logger)
        {
            _musicianServiceValidator = musicianServiceValidator;
            _subscriberRepository = subscriberRepository;
            _logger = logger;
        }

        public async Task Handle(UnsubscribeFromMusicianCommand request, CancellationToken cancellationToken)
        {
            await _musicianServiceValidator.CheckIfMusicianExistsAsync(request.musicianId, cancellationToken);
            await _musicianServiceValidator.CheckIfMusicianExistsAsync(request.musicianToUnsubscribeId, cancellationToken);

            if (!await _subscriberRepository.IsAlreadySubscriberAsync(request.musicianToUnsubscribeId, request.musicianId))
                throw new MusicianNotFoundInSubscribersException(request.musicianToUnsubscribeId, request.musicianId);

            await _subscriberRepository.DeleteFromSubscribersAsync(request.musicianToUnsubscribeId, request.musicianId);

            _logger.LogInformation("Musician with {UnsubId} id successfully was unsubscribed from {MusicianId}", request.musicianId, request.musicianToUnsubscribeId);
        }
    }
}
