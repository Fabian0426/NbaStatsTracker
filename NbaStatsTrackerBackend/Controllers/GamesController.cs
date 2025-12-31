using MediatR;
using Microsoft.AspNetCore.Mvc;
using NbaStatsTrackerBackend.Application.Errors;
using NbaStatsTrackerBackend.Application.Queries.GetAllGames;
using NbaStatsTrackerBackend.Application.Queries.GetASpecificGame;
using NbaStatsTrackerBackend.Application.UseCases.GetASpecificTeam;

namespace NbaStatsTrackerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GamesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllGamesResponse>> GetGames(
            [FromQuery] int? cursor,
            [FromQuery] int? per_page,
            [FromQuery] List<DateTime>? dates,
            [FromQuery] List<int>? seasons,
            [FromQuery] List<int>? team_ids,
            [FromQuery] bool? postseason,
            [FromQuery] DateTime? start_date,
            [FromQuery] DateTime? end_date)
        {
            try
            {
                var request = new GetAllGamesRequest(
                    cursor,
                    per_page,
                    dates,
                    seasons,
                    team_ids,
                    postseason,
                    start_date,
                    end_date
                );

                var response = await _mediator.Send(request, HttpContext.RequestAborted);
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, Errors.GamesNotFound); 
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetASpecificGameResponse>> GetASpecificGame(
            [FromRoute] int id)
        {
            try
            {
                var request = new GetASpecificGameRequest(id);
                var response = await _mediator.Send(request, HttpContext.RequestAborted);

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, Errors.SpecificGameNotFound);
            }
        }
    }
}