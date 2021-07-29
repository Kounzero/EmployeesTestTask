using AutoMapper;
using EmployeesAPI.Caching;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Positions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
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

        /// <summary>
        /// Получение списка всех должностей.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<PositionDto>> GetPositions()
        {
            List<Position> positions;

            if (!_cache.TryGetValue(ChacheKeys.Positions, out positions))
            {
                positions = await _context.Position.ToListAsync();
                _cache.Set(ChacheKeys.Positions, positions);
            }

            return _mapper.Map<List<PositionDto>>(positions);
        }
    }
}
