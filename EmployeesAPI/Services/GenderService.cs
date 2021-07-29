using AutoMapper;
using EmployeesAPI.Caching;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Genders;
using EmployeesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    ///<inheritdoc cref="IGenderService"/>
    public class GenderService : IGenderService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private IMemoryCache _cache;

        public GenderService(DatabaseContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<GenderDto>> GetGenders()
        {
            List<GenderDto> result;

            if (!_cache.TryGetValue(CacheKeys.Genders, out result))
            {
                var genders = await _context.Gender.ToListAsync();
                result = _mapper.Map<List<GenderDto>>(genders);
                _cache.Set(CacheKeys.Genders, result);
            }

            return result;
        }
    }
}
