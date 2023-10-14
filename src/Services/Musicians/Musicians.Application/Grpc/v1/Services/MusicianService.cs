using Grpc.Core;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Musicians.Application.MediatoR.Features.Friends.Queries.GetAllFriendsByGivenMusicianId;
using Musicians.Application.MediatoR.Features.Musicians.Queries.GetByID;
using Musicians.Domain.Exceptions.BadRequestException;
using static Musicians.Application.Grpc.v1.GetMusicianFriendsResponse.Types;

namespace Musicians.Application.Grpc.v1.Services
{
    public class MusicianService : Musician.MusicianBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MusicianService(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<GetMusicianInformationResponse> GetMusicianProfile(GetMusicianInformationRequest request, ServerCallContext context)
        {
            var id = CheckIfGuidIsValidAndGet(request.Id);

            var getMusicianByIdQuery = new GetMusicianByIdQuery(id);
            var musicianResponseDto = await _mediator.Send(getMusicianByIdQuery, context.CancellationToken);

            var response = _mapper.Map<GetMusicianInformationResponse>(musicianResponseDto);

            return response;
        }

        public override async Task<GetMusicianFriendsResponse> GetMusicianFriends(GetMusicianFriendsRequest request, ServerCallContext context)
        {
            var id = CheckIfGuidIsValidAndGet(request.Id);

            var query = new GetAllFriendsByGivenMusicianIdQuery(id);

            var friendsResponseDtos = await _mediator.Send(query, context.CancellationToken);

            var musicianInformations = _mapper.Map<IEnumerable<MusicianFriendInfo>>(friendsResponseDtos);
            var response = new GetMusicianFriendsResponse();
            response.Friends.AddRange(musicianInformations);

            return response;
        }

        private Guid CheckIfGuidIsValidAndGet(string guidValue)
        {
            Guid id;

            var isValid = Guid.TryParse(guidValue, out id);

            if (!isValid)
            {
                throw new InvalidFormatBadRequestException<Guid>(guidValue);
            }

            return id;
        }
    }
}
