using AutoMapper;
using EmployeesAPI.Caching;

using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Employees;
using EmployeesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmployeesAPI.Services
{
    ///<inheritdoc cref="IEmployeeService"/>
    public class EmployeeService : IEmployeeService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private IMemoryCache _cache;

        public EmployeeService(DatabaseContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        ///<inheritdoc/>
        public async Task<List<EmployeeDto>> GetEmployees(int subdivisionId)
        {
            List<EmployeeDto> result;

            var subdivisions = new List<Subdivision>();
            subdivisions.Add(await _context.Subdivision
                .Include(x => x.Subdivisions)
                .Include(x => x.Employees).ThenInclude(x => x.Position)
                .Include(x => x.Employees).ThenInclude(x => x.Gender)
                .FirstOrDefaultAsync(x => x.Id == subdivisionId));


            for (int i = 0; i < subdivisions.Count; i++)
            {
                var children = await _context.Subdivision
                    .Where(x => x.ParentId == subdivisions[i].Id)
                    .Include(x => x.Subdivisions)
                    .Include(x => x.Employees).ThenInclude(x => x.Position)
                    .Include(x => x.Employees).ThenInclude(x => x.Gender)
                    .ToListAsync();

                if (children.Any())
                {
                    subdivisions.AddRange(children);
                }
            }

            result = new List<EmployeeDto>();

            foreach (var subdivision in subdivisions)
            {
                var chacheKey = CacheKeys.EmployeesBySubdivision + subdivision.Id;
                List<EmployeeDto> employees;

                if (!_cache.TryGetValue(chacheKey, out employees))
                {
                    employees = _mapper.Map<List<EmployeeDto>>(subdivision.Employees);
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSlidingExpiration(TimeSpan.FromMinutes(15));
                    _cache.Set(chacheKey, employees);
                }

                result.AddRange(employees);
            }

            return result;
        }

        ///<inheritdoc/>
        public async Task<int> EditEmployee(EditEmployeeDto editEmployeeDto)
        {
            var employee = await _context.Employee.FindAsync(editEmployeeDto.Id);

            if (employee == null)
            {
                return 1;
            }

            _cache.Remove(CacheKeys.EmployeesBySubdivision + employee.SubdivisionId);
            employee.BirthDate = editEmployeeDto.BirthDate;
            employee.FullName = editEmployeeDto.FullName;
            employee.GenderId = editEmployeeDto.GenderId;
            employee.HasDrivingLicense = editEmployeeDto.HasDrivingLicense;
            employee.PositionId = editEmployeeDto.PositionId;
            employee.SubdivisionId = editEmployeeDto.SubdivisionId;

            try
            {
                await _context.SaveChangesAsync();

                _cache.Remove(CacheKeys.EmployeesBySubdivision + editEmployeeDto.SubdivisionId);
            }
            catch (DbUpdateConcurrencyException)
            {
                return 2;
            }

            return 0;
        }

        ///<inheritdoc/>
        public async Task<int> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var newEmployee = new Employee()
            {
                FullName = addEmployeeDto.FullName,
                BirthDate = addEmployeeDto.BirthDate,
                GenderId = addEmployeeDto.GenderId,
                PositionId = addEmployeeDto.PositionId,
                SubdivisionId = addEmployeeDto.SubdivisionId,
                HasDrivingLicense = addEmployeeDto.HasDrivingLicense
            };

            _context.Employee.Add(newEmployee);

            try
            {
                await _context.SaveChangesAsync();
                _cache.Remove(CacheKeys.EmployeesBySubdivision + addEmployeeDto.SubdivisionId);
            }
            catch (DbUpdateConcurrencyException)
            {
                return 2;
            }

            return 0;
        }

        ///<inheritdoc/>
        public async Task<int> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return 1;
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            _cache.Remove(CacheKeys.EmployeesBySubdivision + employee.SubdivisionId);

            return 0;
        }
    }
}
