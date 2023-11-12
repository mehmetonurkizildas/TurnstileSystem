using Application.Features.MovementReports.Queries.GetMovementReportQuery;
using Application.Features.Movements.Command.EnterMovement;
using Application.Features.Movements.Command.ExitMovement;
using Application.Features.Movements.Queries.GetByIdMovement;
using Application.Features.Movements.Queries.GetByPersonIdMovement;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("movements")]
    [ApiController]
    public class MovementController : BaseController
    {

        [HttpPost("enter")]
        public async Task<IActionResult> RecordEntry([FromBody] EnterMovementCommand enterMovementCommand)
        {
            var result = await Mediator!.Send(enterMovementCommand);
            return Ok(result);
        }

        [HttpPost("exit")]
        public async Task<IActionResult> RecordExit([FromBody] ExitMovementCommand exitMovementCommand)
        {
            var result = await Mediator!.Send(exitMovementCommand);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovementById([FromRoute] GetByIdMovementQuery getByIdMovementQuery)
        {
            var result = await Mediator!.Send(getByIdMovementQuery);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovements([FromQuery] GetByPersonIdMovementQuery getByPersonIdMovementQuery)
        {
            var movements = await Mediator!.Send(getByPersonIdMovementQuery);
            return Ok(movements);
        }

        [HttpGet("reports")]
        public async Task<IActionResult> GetReports([FromQuery] GetMovementReportQuery getMovementReportQuery)
        {
            var movementReports = await Mediator!.Send(getMovementReportQuery);
            return Ok(movementReports);
        }
    }

}