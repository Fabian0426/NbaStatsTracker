using MediatR;
using Microsoft.AspNetCore.Mvc;

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


    }
}
