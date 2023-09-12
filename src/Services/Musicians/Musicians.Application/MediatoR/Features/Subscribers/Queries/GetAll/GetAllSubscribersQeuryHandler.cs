using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Musicians.Application.DTOs.ResponseDTOs;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;

namespace Musicians.Application.MediatoR.Features.Subscribers.Queries.GetAll
{
    internal class GetAllSubscribersQeuryHandler
        : IRequestHandler<GetAllSubScribersQuery, IEnumerable<MusicianResponseDto>>
    {
        private readonly ISubcribersRepository _subscribersRepository;
        private readonly IMusicianServiceHelper _musicianServiceHelper;
        private readonly ILogger<GetAllSubscribersQeuryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllSubscribersQeuryHandler(ISubcribersRepository subscribersRepository,
            IMusicianServiceHelper musicianServiceHelper,
            IMapper mapper,
            ILogger<GetAllSubscribersQeuryHandler> logger)
        {
            _subscribersRepository = subscribersRepository;
            _musicianServiceHelper = musicianServiceHelper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MusicianResponseDto>> Handle(GetAllSubScribersQuery request, CancellationToken cancellationToken)
        {
            await _musicianServiceHelper.CheckIfMusicianExistsAsync(request.musicianId, cancellationToken);

            var subscribers = await _subscribersRepository.GetAllMusicianSubscribersAsync(request.musicianId, cancellationToken);

            var subScribersDtos = _mapper.Map<IEnumerable<MusicianResponseDto>>(subscribers);

            _logger.LogInformation("All subscribers of the musician with {Id} id was successfully retrieved", request.musicianId);

            return subScribersDtos;
        }
    }
}
