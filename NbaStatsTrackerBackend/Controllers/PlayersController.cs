using MediatR;
using Microsoft.AspNetCore.Mvc;
using NbaStatsTrackerBackend.Application.Errors;
using NbaStatsTrackerBackend.Application.Queries.GetAllPlayers;

namespace NbaStatsTrackerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlayersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<GetAllPlayersResponse>> GetPlayers
            (
            [FromQuery] int? cursor,
            [FromQuery] int? per_page,
            [FromQuery] string? search,
            [FromQuery] string? first_name,
            [FromQuery] string? last_name,
            [FromQuery] List<int>? team_ids,
            [FromQuery] List<int>? player_ids
        )
        {
            try
            {
                var request = new GetAllPlayersRequest(
                    cursor,
                    per_page,
                    search,
                    first_name,
                    last_name,
                    team_ids ?? new List<int>(),
                    player_ids ?? new List<int>()
                );
                var response = await _mediator.Send(request, HttpContext.RequestAborted);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, Errors.PlayersNotFound);
            }
        }

    }
}
