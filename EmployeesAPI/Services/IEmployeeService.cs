using EmployeesAPI.Entities;
using EmployeesAPI.Models.Dtos.Employees;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeDto>> GetEmployees(int subdivisionId);
        public Task<int> EditEmployee(EditEmployeeDto editEmployeeDto);
        public Task<int> AddEmployee(AddEmployeeDto addEmployeeDto);
        public Task<int> DeleteEmployee(int id);
    }
}
