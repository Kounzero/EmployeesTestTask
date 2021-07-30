using AutoMapper;
using EmployeesAPI.Caching;

using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Subdivisions;
using EmployeesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    ///<inheritdoc cref="ISubdivisionService"/>
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

        ///<inheritdoc/>
        public async Task<List<SubdivisionDto>> GetAllSubdivisions()
        {
            List<SubdivisionDto> result;

            if (_cache.TryGetValue(CacheKeys.AllSubdivisions, out result))
            {
                return result;
            }

            var subdivisions = await _context.Subdivision.ToListAsync();
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetPriority(CacheItemPriority.Normal)
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            result = _mapper.Map<List<SubdivisionDto>>(subdivisions).OrderBy(x => x.Title).ToList();
            _cache.Set(CacheKeys.AllSubdivisions, result, cacheEntryOptions);

            return result;
        }

        ///<inheritdoc/>
        public async Task<List<SubdivisionDto>> GetSubdivisions(int? parentSubdivisionId)
        {
            List<SubdivisionDto> result;
            var chacheKey = CacheKeys.SubdivisionsByParent + parentSubdivisionId;

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

        ///<inheritdoc/>
        public async Task<int> EditSubdivision(EditSubdivisionDto editSubdivisionDto)
        {
            var subdivision = await _context.Subdivision.FindAsync(editSubdivisionDto.Id);

            if (subdivision == null)
            {
                return 1;
            }

            if (editSubdivisionDto.ParentId.HasValue && !(await CheckSubdivisionParentingPossibility(editSubdivisionDto.Id, (int)editSubdivisionDto.ParentId)))
            {
                return 2;
            }

            _cache.Remove(CacheKeys.SubdivisionsByParent + subdivision.ParentId);
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

            _cache.Remove(CacheKeys.AllSubdivisions);
            _cache.Remove(CacheKeys.SubdivisionsByParent + editSubdivisionDto.ParentId);
            _cache.Remove(CacheKeys.EmployeesBySubdivision + editSubdivisionDto.Id);

            return 0;
        }

        ///<inheritdoc/>
        public async Task<int> AddSubdivision(AddSubdivisionDto addSubdivisionDto)
        {
            if (!addSubdivisionDto.ParentId.HasValue && await _context.Subdivision.FindAsync(addSubdivisionDto.ParentId) == null)
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

            _cache.Remove(CacheKeys.AllSubdivisions);
            _cache.Remove(CacheKeys.SubdivisionsByParent + addSubdivisionDto.ParentId);

            return 0;
        }

        ///<inheritdoc/>
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
                    _cache.Remove(CacheKeys.SubdivisionsByParent + children[i].ParentId);
                    _cache.Remove(CacheKeys.EmployeesBySubdivision + children[i].Id);
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

            _cache.Remove(CacheKeys.AllSubdivisions);

            return 0;
        }

        ///<inheritdoc/>
        public async Task<bool> CheckSubdivisionParentingPossibility(int subdivisionId, int targetSubdivisionId)
        {
            var children = await GetAllSubdivisionChildren(subdivisionId);

            return children.FirstOrDefault(x => x.Id == targetSubdivisionId) == null;
        }

        ///<inheritdoc/>
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
