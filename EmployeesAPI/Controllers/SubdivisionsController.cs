
using EmployeesAPI.Models.Dtos.Subdivisions;
using EmployeesAPI.Models.Entities;
using EmployeesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubdivisionsController : ControllerBase
    {
        private ISubdivisionService SubdivisionService;

        public SubdivisionsController(ISubdivisionService subdivisionService)
        {
            SubdivisionService = subdivisionService;
        }

        // Get all subdivisions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubdivisionDto>>> GetAllSubdivisions()
        {
            return Ok(await SubdivisionService.GetAllSubdivisions());
        }

        // Get all subdivisions by parent
        [HttpGet("GetSubdivisions")]
        public async Task<ActionResult<IEnumerable<SubdivisionDto>>> GetSubdivisions(int? parentSubdivisionId)
        {
            var result = await SubdivisionService.GetSubdivisions(parentSubdivisionId);
            return Ok(result);
        }

        // Change subdivision's data
        [HttpPut]
        public async Task<IActionResult> PutSubdivision([FromBody] EditSubdivisionDto editSubdivisionDto)
        {
            switch (await SubdivisionService.EditSubdivision(editSubdivisionDto))
            {
                case 0:
                    return Ok();
                case 1:
                    return NotFound("Подразделение не найдено");
                case 2:
                    return BadRequest("Невозможно изменить родительское подразделение, т.к. целевое родительское подразделение является дочерним для изменяемого");
                case 3:
                    return BadRequest("Ошибка сохранения данных");

                default:
                    return BadRequest();
            }
        }

        // Create new subdivision
        [HttpPost]
        public async Task<ActionResult<Subdivision>> PostSubdivision([FromBody] AddSubdivisionDto addSubdivisionDto)
        {
            switch (await SubdivisionService.AddSubdivision(addSubdivisionDto))
            {
                case 0:
                    return Ok();
                case 1:
                    return NotFound("Указанное родительское подразделение не найдено");

                default:
                    return BadRequest();
            }
        }

        // Delete subdivision
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubdivision(int id)
        {
            switch (await SubdivisionService.DeleteSubdivision(id))
            {
                case 0:
                    return Ok();
                case 1:
                    return NotFound("Подразделение не найдено");

                default:
                    return BadRequest();
            }
        }
    }
}
