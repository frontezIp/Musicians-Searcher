using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Musicians.Application.DTOs.ResponseDTOs;
using Musicians.Application.Interfaces.Persistance;

namespace Musicians.Application.MediatoR.Features.Genres.Queries.GetAll
{
    public class GetAllGenresQueryHandler :
        IRequestHandler<GetAllGenresQuery, IEnumerable<GenreResponseDto>>
    {
        private readonly IMapper _mapper;

        private readonly IGenresRepository _genresRepository;

        private readonly ILogger<GetAllGenresQueryHandler> _logger;

        public GetAllGenresQueryHandler(IMapper mapper,
            IGenresRepository genresRepository,
            ILogger<GetAllGenresQueryHandler> logger)
        {
            _mapper = mapper;
            _genresRepository = genresRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<GenreResponseDto>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _genresRepository.GetAllAsync(cancellationToken);

            var genreDtos = _mapper.Map<IEnumerable<GenreResponseDto>>(genres);

            _logger.LogInformation("All genres was successfully retrieved");

            return genreDtos;   
        }
    }
}
