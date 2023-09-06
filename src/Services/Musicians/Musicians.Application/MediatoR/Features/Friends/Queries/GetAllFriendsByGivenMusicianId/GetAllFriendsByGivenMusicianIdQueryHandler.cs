using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Musicians.Application.DTOs.ResponseDTOs;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;

namespace Musicians.Application.MediatoR.Features.Friends.Queries.GetAllFriendsByGivenMusicianId
{
    public class GetAllFriendsByGivenMusicianIdQueryHandler
        : IRequestHandler<GetAllFriendsByGivenMusicianIdQuery, IEnumerable<MusicianResponseDto>>
    {
        private readonly IFriendsRepository _friendsRepository;
        private readonly IMapper _mapper;
        private readonly IMusicianServiceHelper _musicianServiceValidator;
        private readonly ILogger<GetAllFriendsByGivenMusicianIdQuery> _logger;

        public GetAllFriendsByGivenMusicianIdQueryHandler(IFriendsRepository friendsRepository,
            IMapper mapper,
            IMusicianServiceHelper musicianServiceValidator,
            ILogger<GetAllFriendsByGivenMusicianIdQuery> logger)
        {
            _friendsRepository = friendsRepository;
            _mapper = mapper;
            _musicianServiceValidator = musicianServiceValidator;
            _logger = logger;
        }

        public async Task<IEnumerable<MusicianResponseDto>> Handle(GetAllFriendsByGivenMusicianIdQuery request, CancellationToken cancellationToken)
        {
            await _musicianServiceValidator.CheckIfMusicianExistsAsync(request.musicianId, cancellationToken);

            var musicianFriends = await _friendsRepository.GetAllMusicianFriendsAsync(request.musicianId, cancellationToken);

            var musicianFriendsDtos = _mapper.Map<IEnumerable<MusicianResponseDto>>(musicianFriends);

            _logger.LogInformation("All friends of the musician with {Id} id was successfully retrieved", request.musicianId);

            return musicianFriendsDtos;
        }
    }
}
