using MediatR;
using Microsoft.AspNetCore.Mvc;
using Musicians.Application.MediatoR.Features.Subscribers.Commands.Unsubscribe;
using Musicians.Application.MediatoR.Features.Subscribers.Queries.GetAll;

namespace Musicians.API.Controllers.v1
{
    [Route("api/{v:apiversion}/Musicians/{musicianId}/[controller]")]
    public class SubscribersController : BaseApiController
    {
        private readonly IMediator _mediator;

        public SubscribersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSubscribersAsync(Guid musicianId, CancellationToken cancellationToken)
        {
            var query = new GetAllSubScribersQuery(musicianId);

            var result = await _mediator.Send(query,cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{musicianToUnsubId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnsubscribeAsync(Guid musicianId, Guid musicianToUnsubId, CancellationToken cancellationToken)
        {
            var query = new UnsubscribeFromMusicianCommand(musicianId, musicianToUnsubId);

            await _mediator.Send(query, cancellationToken);

            return NoContent();
        }
    }
}
