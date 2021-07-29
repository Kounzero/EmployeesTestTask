using AutoMapper;
using EmployeesAPI.Caching;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Subdivisions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    public class SubdivisionService : ISubdivisionService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private IMemoryCache _cache;

        public SubdivisionService(DatabaseContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        /// <summary>
        /// Получение всех подразделений
        /// </summary>
        /// <returns></returns>
        public async Task<List<SubdivisionDto>> GetAllSubdivisions()
        {
            List<SubdivisionDto> result;

            if (_cache.TryGetValue(ChacheKeys.AllSubdivisions, out result))
            {
                return result;
            }

            var subdivisions = await _context.Subdivision.ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.Normal)
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            result = _mapper.Map<List<SubdivisionDto>>(subdivisions).OrderBy(x => x.Title).ToList();
            _cache.Set(ChacheKeys.AllSubdivisions, result, cacheEntryOptions);

            return result;
        }

        /// <summary>
        /// Получение всех дочерних подразделений первого уровня вложенности по идентификатору родительского
        /// </summary>
        /// <param name="parentSubdivisionId">Идентификатор родительского подразделения</param>
        /// <returns></returns>
        public async Task<List<SubdivisionDto>> GetSubdivisions(int? parentSubdivisionId)
        {
            List<SubdivisionDto> result;
            var chacheKey = ChacheKeys.SubdivisionsByParent + parentSubdivisionId;

            if (_cache.TryGetValue(chacheKey, out result))
            {
                return result;
            }

            var subdivisions = await _context.Subdivision
                .Where(x => x.ParentId == parentSubdivisionId)
                .Include(x => x.Subdivisions)
                .ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.High)
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            result = _mapper.Map<List<SubdivisionDto>>(subdivisions).OrderBy(x => x.Title).ToList();
            _cache.Set(chacheKey, result, cacheEntryOptions);

            return result;
        }

        /// <summary>
        /// Изменение данных о подразделении
        /// </summary>
        /// <param name="editSubdivisionDto">Модель изменяемого подразделения</param>
        /// <returns>Статус код выполнения функции, где:
        /// 0 - выполнена успешно;
        /// 1 - подразделение не найдено;
        /// 2 - невозможно изменить родительское подразделение, т.к. целевое родительское подразделение является дочерним для изменяемого.
        /// 3 - ошибка сохранения данных.</returns>
        public async Task<int> EditSubdivision(EditSubdivisionDto editSubdivisionDto)
        {
            var subdivision = await _context.Subdivision.FindAsync(editSubdivisionDto.Id);

            if (subdivision == null)
            {
                return 1;
            }

            if (editSubdivisionDto.ParentId != null && !(await CheckSubdivisionParentingPossibility(editSubdivisionDto.Id, (int)editSubdivisionDto.ParentId)))
            {
                return 2;
            }

            _cache.Remove(ChacheKeys.SubdivisionsByParent + subdivision.ParentId);
            subdivision.ParentId = editSubdivisionDto.ParentId;
            subdivision.Title = editSubdivisionDto.Title;
            subdivision.Description = editSubdivisionDto.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 3;
            }

            _cache.Remove(ChacheKeys.AllSubdivisions);
            _cache.Remove(ChacheKeys.SubdivisionsByParent + editSubdivisionDto.ParentId);
            _cache.Remove(ChacheKeys.EmployeesBySubdivision + editSubdivisionDto.Id);

            return 0;
        }

        /// <summary>
        /// Создание нового подразделения
        /// </summary>
        /// <param name="addSubdivisionDto">Модель создаваемого подразделения</param>
        /// <returns>Статус код выполнения функции, где:
        /// 0 - выполнена успешно;
        /// 1 - указанное родительское подразделение не найдено;
        /// 3 - ошибка сохранения данных.</returns>
        public async Task<int> AddSubdivision(AddSubdivisionDto addSubdivisionDto)
        {
            if (addSubdivisionDto.ParentId != null && await _context.Subdivision.FindAsync(addSubdivisionDto.ParentId) == null)
            {
                return 1;
            }

            _context.Subdivision.Add(new Subdivision()
            {
                FormDate = DateTime.Now,
                Description = addSubdivisionDto.Description,
                ParentId = addSubdivisionDto.ParentId,
                Title = addSubdivisionDto.Title
            });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 3;
            }

            _cache.Remove(ChacheKeys.AllSubdivisions);
            _cache.Remove(ChacheKeys.SubdivisionsByParent + addSubdivisionDto.ParentId);

            return 0;
        }

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Модель создаваемого подразделения</param>
        /// <returns>Статус код выполнения функции, где:
        /// 0 - выполнена успешно;
        /// 1 - подразделение не найдено;
        /// 3 - ошибка сохранения данных.</returns>
        public async Task<int> DeleteSubdivision(int id)
        {
            var subdivision = await _context.Subdivision.FindAsync(id);

            if (subdivision == null)
            {
                return 1;
            }

            var children = await GetAllSubdivisionChildren(id);

            if (children.Any())
            {
                for (int i = children.Count - 1; i >= 0; i--)
                {
                    var forDelete = await _context.Subdivision.FindAsync(children[i].Id);
                    _context.Subdivision.Remove(forDelete);
                    _cache.Remove(ChacheKeys.SubdivisionsByParent + children[i].ParentId);
                    _cache.Remove(ChacheKeys.EmployeesBySubdivision + children[i].Id);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 3;
            }

            _cache.Remove(ChacheKeys.AllSubdivisions);

            return 0;
        }

        /// <summary>
        /// Метод для определения, можно ли сделать одно подразделение дочерним для другого. Необходимо для избежания циклических зависимостей подразделений
        /// </summary>
        /// <param name="subdivisionId">Идентификатор проверяемого подразделения</param>
        /// <param name="targetSubdivisionId">Идентификатор целевого подразделения</param>
        /// <returns>Возвращает true, если возможно, иначе false</returns>
        public async Task<bool> CheckSubdivisionParentingPossibility(int subdivisionId, int targetSubdivisionId)
        {
            var children = await GetAllSubdivisionChildren(subdivisionId);

            return children.FirstOrDefault(x => x.Id == targetSubdivisionId) == null;
        }

        /// <summary>
        /// Метод для получения всех вложенных подразделений указанного подразделения
        /// </summary>
        /// <param name="subdivisionId">Идентификатор родительского подразделения</param>
        /// <returns></returns>
        public async Task<List<SubdivisionDto>> GetAllSubdivisionChildren(int subdivisionId)
        {
            var subdivisions = new List<Subdivision>();
            subdivisions.Add(await _context.Subdivision
                .Include(x => x.Subdivisions)
                .FirstOrDefaultAsync(x => x.Id == subdivisionId));

            for (int i = 0; i < subdivisions.Count; i++)
            {
                var children = await _context.Subdivision
                    .Where(x => x.ParentId == subdivisions[i].Id)
                    .Include(x => x.Subdivisions)
                    .ToListAsync();

                if (children.Any())
                {
                    subdivisions.AddRange(children);
                }
            }

            return _mapper.Map<List<SubdivisionDto>>(subdivisions);
        }
    }
}
