using AutoMapper;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeesAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get, получение сотрудников из указанного подразделения и всех его вложенных
        public async Task<List<EmployeeDto>> GetEmployees(int subdivisionId)
        {
            var subdivisions = new List<Subdivision>();
            subdivisions.Add(_context.Subdivision
                .Include(x => x.Subdivisions)
                .Include(x => x.Employees).ThenInclude(x => x.Position)
                .Include(x => x.Employees).ThenInclude(x => x.Gender)
                .FirstOrDefault(x => x.Id == subdivisionId));


            for (int i = 0; i < subdivisions.Count; i++)
            {
                var children = _context.Subdivision
                    .Where(x => x.ParentId == subdivisions[i].Id)
                    .Include(x => x.Subdivisions)
                    .Include(x => x.Employees).ThenInclude(x => x.Position)
                    .Include(x => x.Employees).ThenInclude(x => x.Gender)
                    .ToList();
                if (children.Any())
                {
                    subdivisions.AddRange(children);
                }
            }

            var result = new List<EmployeeDto>();

            foreach (var subdivision in subdivisions)
            {
                result.AddRange(_mapper.Map<List<EmployeeDto>>(subdivision.Employees));
            }

            return result;
        }

        // Put, изменение информации о сотрудниках
        public async Task<int> PutEmployee(EditEmployeeDto editEmployeeDto)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(x => x.Id == editEmployeeDto.Id);

            if (employee == null)
            {
                return 1; //Сотрудник не найден
            }

            employee.BirthDate = editEmployeeDto.BirthDate;
            employee.FullName = editEmployeeDto.FullName;
            employee.GenderID = editEmployeeDto.GenderId;
            employee.HasDrivingLicense = editEmployeeDto.HasDrivingLicense;
            employee.PositionID = editEmployeeDto.PositionId;
            employee.SubdivisionId = editEmployeeDto.SubdivisionId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 2; //Исключение сохранения данных
            }

            return 0; //Всё ок
        }

        // Post, добавление сотрудника
        public async Task<int> PostEmployee(AddEmployeeDto addEmployeeDto)
        {

            _context.Employee.Add(_mapper.Map<Employee>(addEmployeeDto));
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 2; //Исключение сохранения данных
            }

            return 0;
        }

        // Delete, удаление сотрудника
        public async Task<int> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return 1;
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return 0;
        }
    }
}
