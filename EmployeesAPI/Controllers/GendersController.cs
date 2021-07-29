using EmployeesAPI.Models.Dtos.Genders;
using EmployeesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private IGenderService GenderService;

        public GendersController(IGenderService genderService)
        {
            GenderService = genderService;
        }

        // Get all genders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenderDto>>> GetGenders()
        {
            return Ok(await GenderService.GetGenders());
        }
    }
}
