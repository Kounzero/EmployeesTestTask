using AutoMapper;
using EmployeesAPI.Caching;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Positions;
using EmployeesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    ///<inheritdoc cref="IPositionService"/>
    public class PositionService : IPositionService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private IMemoryCache _cache;

        public PositionService(DatabaseContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<PositionDto>> GetPositions()
        {
            List<Position> positions;

            if (!_cache.TryGetValue(CacheKeys.Positions, out positions))
            {
                positions = await _context.Position.ToListAsync();
                _cache.Set(CacheKeys.Positions, positions);
            }

            return _mapper.Map<List<PositionDto>>(positions);
        }
    }
}
