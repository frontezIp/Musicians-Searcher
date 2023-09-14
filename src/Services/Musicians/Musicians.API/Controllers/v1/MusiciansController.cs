using MediatR;
using Microsoft.AspNetCore.Mvc;
using Musicians.Application.DTOs.RequestDTOs;
using Musicians.Application.MediatoR.Features.Musicians.Commands.UpdateMusicianProfile;
using Musicians.Application.MediatoR.Features.Musicians.Queries.GetByID;
using Musicians.Application.MediatoR.Features.Musicians.Queries.GetFilteredMusicians;
using System.Text.Json;

namespace Musicians.API.Controllers.v1
{
    public class MusiciansController : BaseApiController
    {
        private readonly IMediator _mediator;

        public MusiciansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{musicianId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMusicianByIdAsync(Guid musicianId, CancellationToken cancellationToken)
        {
            var query = new GetMusicianByIdQuery(musicianId);

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFilteredMusicians([FromQuery]GetFilteredMusiciansRequestDto request, CancellationToken cancellationToken)
        {
            var query = new GetFilteredMusiciansQuery(request);

            var result = await _mediator.Send(query, cancellationToken);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(result.metaData));

            return Ok(result.musicians);
        }

        [HttpPut("{musicianId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CurrentMusicianProfile(Guid musicianId, [FromBody]UpdateMusicianProfileRequestDto updateMusicianProfileRequestDto, CancellationToken cancellationToken)
        {
            var query = new UpdateMusicianProfileCommand(updateMusicianProfileRequestDto, musicianId);

            await _mediator.Send(query, cancellationToken);

            return NoContent();
        }
    }
}
