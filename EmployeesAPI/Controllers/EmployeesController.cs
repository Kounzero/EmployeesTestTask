using EmployeesAPI.Models.Dtos.Employees;
using EmployeesAPI.Models.Entities;
using EmployeesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeService EmployeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            EmployeeService = employeeService;
        }

        // Get, получение сотрудников из указанного подразделения и всех его вложенных
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDto>>> GetEmployees(int subdivisionId)
        {
            return await EmployeeService.GetEmployees(subdivisionId);
        }

        // Put, изменение информации о сотрудниках
        [HttpPut]
        public async Task<IActionResult> PutEmployee([FromBody] EditEmployeeDto editEmployeeDto)
        {
            return await EmployeeService.EditEmployee(editEmployeeDto) switch
            {
                ServiceResult.Ok => Ok(),
                ServiceResult.NotFound => NotFound("Сотрудник не найден"),
                ServiceResult.DataSavingError => BadRequest("Ошибка сохранения данных"),
                _ => BadRequest(),
            };
        }

        // Post, добавление сотрудника
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            return await EmployeeService.AddEmployee(addEmployeeDto) switch
            {
                ServiceResult.Ok => Ok(),
                ServiceResult.DataSavingError => BadRequest("Ошибка сохранения данных"),
                _ => BadRequest(),
            };
        }

        // Delete, удаление сотрудника
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            return await EmployeeService.DeleteEmployee(id) switch
            {
                ServiceResult.Ok => Ok(),
                ServiceResult.NotFound => NotFound("Сотрудник не найден"),
                ServiceResult.DataSavingError => BadRequest("ошибка сохранения данных"),
                _ => BadRequest(),
            };
        }
    }
}
