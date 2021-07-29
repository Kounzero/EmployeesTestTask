using EmployeesAPI.Models.Dtos.Positions;
using EmployeesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private IPositionService PositionService;

        public PositionsController(IPositionService positionService)
        {
            PositionService = positionService;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionDto>>> GetPositions()
        {
            return Ok(await PositionService.GetPositions());
        }
    }
}
