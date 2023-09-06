using MediatR;
using Microsoft.AspNetCore.Mvc;
using Musicians.Application.MediatoR.Features.Friends.Commands.AddFriend;
using Musicians.Application.MediatoR.Features.Friends.Commands.DeleteFriend;
using Musicians.Application.MediatoR.Features.Friends.Queries.GetAllFriendsByGivenMusicianId;

namespace Musicians.API.Controllers.v1
{
    [Route("api/{v:apiversion}/Musicians/{musicianId}/[controller]")]
    public class FriendsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFriendsAsync(Guid musicianId, CancellationToken cancellationToken)
        {
            var query = new GetAllFriendsByGivenMusicianIdQuery(musicianId);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddToFriendAsync(Guid musicianId, Guid musicianToAddId)
        {
            var query = new AddFriendToMusicianCommand(musicianId, musicianToAddId);

            await _mediator.Send(query);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteFromFriendsAsync(Guid musicianId, Guid musicianToDeleteFromFriendsId)
        {
            var query = new DeleteFriendCommand(musicianId, musicianToDeleteFromFriendsId);

            await _mediator.Send(query);

            return NoContent();
        }
    }
}
