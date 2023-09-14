using MediatR;
using Microsoft.AspNetCore.Mvc;
using Musicians.Application.MediatoR.Features.Genres.Queries.GetAll;

namespace Musicians.API.Controllers.v1
{
    public class GenresController : BaseApiController
    {
        private readonly IMediator _mediator;

        public GenresController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGenresAsync(CancellationToken cancellationToken)
        {
            var query = new GetAllGenresQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
