using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Subdivisions;
using AutoMapper;
using EmployeesAPI.Models.Dtos.Employees;
using EmployeesAPI.Services;

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
            switch (await EmployeeService.PutEmployee(editEmployeeDto))
            {
                case 0:
                    return Ok();
                case 1:
                    return NotFound("Сотрудник не найден");
                case 2:
                    return BadRequest("Ошибка сохранения данных");

                default:
                    return BadRequest();
            }
        }

        // Post, добавление сотрудника
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            switch (await EmployeeService.PostEmployee(addEmployeeDto))
            {
                case 0:
                    return Ok();
                case 2:
                    return BadRequest("Ошибка сохранения данных");

                default:
                    return BadRequest();
            }
        }

        // Delete, удаление сотрудника
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            switch (await EmployeeService.DeleteEmployee(id))
            {
                case 0:
                    return Ok();
                case 1:
                    return NotFound("Сотрудник не найден");

                default:
                    return BadRequest();
            }
        }
    }
}
