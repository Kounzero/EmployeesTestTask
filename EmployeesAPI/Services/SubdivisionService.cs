using AutoMapper;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Subdivisions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public SubdivisionService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all subdivisions
        public async Task<List<SubdivisionDto>> GetAllSubdivisions()
        {
            var subdivisions = await _context.Subdivision.ToListAsync();

            return _mapper.Map<List<SubdivisionDto>>(subdivisions).OrderBy(x => x.Title).ToList();
        }

        // Get all subdivisions by parent
        public async Task<List<SubdivisionDto>> GetSubdivisions(int? parentSubdivisionId)
        {
            var subdivisions = await _context.Subdivision
                .Where(x => x.ParentId == parentSubdivisionId)
                .Include(x => x.Subdivisions)
                .ToListAsync();

            return _mapper.Map<List<SubdivisionDto>>(subdivisions).OrderBy(x => x.Title).ToList();
        }

        // Change subdivision's data
        public async Task<int> PutSubdivision(EditSubdivisionDto editSubdivisionDto)
        {
            var subdivision = await _context.Subdivision.FindAsync(editSubdivisionDto.Id);

            if (subdivision == null)
            {
                return 1; //Подразделение не найдено
            }

            if (editSubdivisionDto.ParentId != null && !(await CheckSubdivisionParentingPossibility(editSubdivisionDto.Id, (int)editSubdivisionDto.ParentId)))
            {
                return 2; //Невозможно изменить родительское подразделение, т.к. целевое родительское подразделение является дочерним для изменяемого
            }

            subdivision.ParentId = editSubdivisionDto.ParentId;
            subdivision.Title = editSubdivisionDto.Title;
            subdivision.Description = editSubdivisionDto.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 3; //Ошибка сохранения данных
            }

            return 0;
        }

        // Create new subdivision
        public async Task<int> PostSubdivision(AddSubdivisionDto addSubdivisionDto)
        {
            if (addSubdivisionDto.ParentId != null && await _context.Subdivision.FindAsync(addSubdivisionDto.ParentId) == null)
            {
                return 1; //Указанное родительское подразделение не найдено
            }

            _context.Subdivision.Add(new Subdivision()
            {
                FormDate = DateTime.Now,
                Description = addSubdivisionDto.Description,
                ParentId = addSubdivisionDto.ParentId,
                Title = addSubdivisionDto.Title
            });
            await _context.SaveChangesAsync();

            return 0;
        }

        // Delete subdivision
        public async Task<int> DeleteSubdivision(int id)
        {
            var subdivision = await _context.Subdivision.FindAsync(id);

            if (subdivision == null)
            {
                return 1; //Подразделение не найдено
            }

            var children = await GetAllSubdivisionChildren(id);

            if (children.Any())
            {
                for (int i = children.Count - 1; i >= 0; i--)
                {
                    var forDelete = await _context.Subdivision.FindAsync(children[i].Id);
                    _context.Subdivision.Remove(forDelete);
                }
            }

            await _context.SaveChangesAsync();

            return 0;
        }

        //Метод для определения, можно ли сделать одно подразделение дочерним для другого. Необходимо для избежания циклических зависимостей подразделений
        //Возвращает true, если возможно, иначе false
        public async Task<bool> CheckSubdivisionParentingPossibility(int subdivisionId, int targetSubdivisionId)
        {
            var children = await GetAllSubdivisionChildren(subdivisionId);

            return children.FirstOrDefault(x => x.Id == targetSubdivisionId) == null;
        }

        //Метод для получения всех вложенных подразделений указанного подразделения
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
