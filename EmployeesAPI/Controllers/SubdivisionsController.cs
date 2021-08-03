
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
            return await SubdivisionService.EditSubdivision(editSubdivisionDto) switch
            {
                ServiceResult.Ok => Ok(),
                ServiceResult.NotFound => NotFound("Подразделение не найдено"),
                ServiceResult.DataProcessionError => BadRequest("Невозможно изменить родительское подразделение, т.к. целевое родительское подразделение является дочерним для изменяемого"),
                ServiceResult.DataSavingError => BadRequest("Ошибка сохранения данных"),
                _ => BadRequest(),
            };
        }

        // Create new subdivision
        [HttpPost]
        public async Task<ActionResult<Subdivision>> PostSubdivision([FromBody] AddSubdivisionDto addSubdivisionDto)
        {
            return await SubdivisionService.AddSubdivision(addSubdivisionDto) switch
            {
                ServiceResult.Ok => Ok(),
                ServiceResult.NotFound => NotFound("Указанное родительское подразделение не найдено"),
                ServiceResult.DataSavingError => BadRequest("ошибка сохранения данных"),
                _ => BadRequest(),
            };
        }

        // Delete subdivision
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubdivision(int id)
        {
            return await SubdivisionService.DeleteSubdivision(id) switch
            {
                ServiceResult.Ok => Ok(),
                ServiceResult.NotFound => NotFound("Подразделение не найдено"),
                ServiceResult.DataSavingError => BadRequest("ошибка сохранения данных"),
                _ => BadRequest(),
            };
        }
    }
}
