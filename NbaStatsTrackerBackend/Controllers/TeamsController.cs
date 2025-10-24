using MediatR;
using Microsoft.AspNetCore.Mvc;
using NbaStatsTrackerBackend.Application.UseCases.GetAllTeams;

namespace NbaStatsTrackerBackend.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class TeamsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<GetAllTeamsResponse>> GetTeams(
            [FromQuery] string? conference,
            [FromQuery] string? division)
        {
            var request = new GetAllTeamsRequest(conference, division);
            var response = await _mediator.Send(request, HttpContext.RequestAborted);

            return Ok(response);
        }
    }
}
