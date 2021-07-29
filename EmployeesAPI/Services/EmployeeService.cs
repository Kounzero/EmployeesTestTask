using AutoMapper;
using EmployeesAPI.Caching;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache _cache;

        public EmployeeService(DatabaseContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        /// <summary>
        /// Получение списка сотрудников из указанного подразделения и всех его вложенных подразделений
        /// </summary>
        /// <param name="subdivisionId">Идентификатор подразделения</param>
        /// <returns></returns>
        public async Task<List<EmployeeDto>> GetEmployees(int subdivisionId)
        {
            List<EmployeeDto> result;
            var chacheKey = ChacheKeys.EmployeesBySubdivision + subdivisionId;

            if (_cache.TryGetValue(chacheKey, out result))
            {
                return result;
            }

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
                result.AddRange(_mapper.Map<List<EmployeeDto>>(subdivision.Employees));
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.Normal)
                .SetSlidingExpiration(TimeSpan.FromMinutes(15));
            _cache.Set(chacheKey, result);

            return result;
        }

        /// <summary>
        /// Изменение информации о сотруднике
        /// </summary>
        /// <param name="editEmployeeDto">Модель изменяемого сотрудника с новыми значениями</param>
        /// <returns>Статус код, где:
        /// 0 - выполнение успешно;
        /// 1 - сотрудник не найден;
        /// 2 - ошибка сохранения данных.</returns>
        public async Task<int> EditEmployee(EditEmployeeDto editEmployeeDto)
        {
            var employee = await _context.Employee.FirstOrDefaultAsync(x => x.Id == editEmployeeDto.Id);

            if (employee == null)
            {
                return 1;
            }

            _cache.Remove(ChacheKeys.EmployeesBySubdivision + employee.SubdivisionId);
            employee.BirthDate = editEmployeeDto.BirthDate;
            employee.FullName = editEmployeeDto.FullName;
            employee.GenderID = editEmployeeDto.GenderId;
            employee.HasDrivingLicense = editEmployeeDto.HasDrivingLicense;
            employee.PositionID = editEmployeeDto.PositionId;
            employee.SubdivisionId = editEmployeeDto.SubdivisionId;

            try
            {
                await _context.SaveChangesAsync();
                _cache.Remove(ChacheKeys.EmployeesBySubdivision + editEmployeeDto.SubdivisionId);
            }
            catch (DbUpdateConcurrencyException)
            {
                return 2;
            }

            return 0;
        }

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="addEmployeeDto">Модель нового сотрудника</param>
        /// <returns>Статус код, где:
        /// 0 - выполнение успешно;
        /// 2 - ошибка сохранения данных.</returns>
        public async Task<int> AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            _context.Employee.Add(_mapper.Map<Employee>(addEmployeeDto));
            try
            {
                await _context.SaveChangesAsync();
                _cache.Remove(ChacheKeys.EmployeesBySubdivision + addEmployeeDto.SubdivisionId);
            }
            catch (DbUpdateConcurrencyException)
            {
                return 2; //Исключение сохранения данных
            }

            return 0;
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <returns>Статус код, где:
        /// 0 - выполнение успешно;
        /// 1 - сотрудник не найден.</returns>
        public async Task<int> DeleteEmployee(int id)
        {
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return 1;
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            _cache.Remove(ChacheKeys.EmployeesBySubdivision + employee.SubdivisionId);

            return 0;
        }
    }
}
