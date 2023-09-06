using MapsterMapper;
using MediatR;
using Musicians.Application.DTOs.ResponseDTOs;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;

namespace Musicians.Application.MediatoR.Features.Musicians.Queries.GetByID
{
    internal class GetMusicianByIdQueryHandler
        : IRequestHandler<GetMusicianByIdQuery, MusicianResponseDto>
    {
        private readonly IMusicianServiceHelper _musicianServiceValidator;
        private readonly IMapper _mapper;

        public GetMusicianByIdQueryHandler(IMapper mapper,
            IMusicianServiceHelper musicianServiceValidator)
        {
            _mapper = mapper;
            _musicianServiceValidator = musicianServiceValidator;
        }

        public async Task<MusicianResponseDto> Handle(GetMusicianByIdQuery request, CancellationToken cancellationToken)
        {
            var musician = await _musicianServiceValidator.CheckIfMusicianExistsAndGetAsync(request.musicianId, cancellationToken);

            var musicianDto = _mapper.Map<MusicianResponseDto>(musician);

            return musicianDto;
        }
    }
}
