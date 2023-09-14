using MediatR;
using Musicians.Application.DTOs.ResponseDTOs;

namespace Musicians.Application.MediatoR.Features.Genres.Queries.GetAll
{
    public record GetAllGenresQuery() :
        IRequest<IEnumerable<GenreResponseDto>>;
}
