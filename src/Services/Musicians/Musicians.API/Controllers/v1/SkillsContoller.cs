using MediatR;
using Microsoft.AspNetCore.Mvc;
using Musicians.Application.MediatoR.Features.Skills.Queries.GetAll;

namespace Musicians.API.Controllers.v1
{
    public class SkillsContoller : BaseApiController
    {
        private readonly IMediator _mediator;

        public SkillsContoller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSkillsAsync(CancellationToken cancellationToken)
        {
            var query = new GetAllSkillsQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
