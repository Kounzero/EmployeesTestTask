using AutoMapper;
using EmployeesAPI.Caching;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Genders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
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

        /// <summary>
        /// Получение всех полов.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<GenderDto>> GetGenders()
        {
            List<Gender> genders;

            if (!_cache.TryGetValue(ChacheKeys.Genders, out genders))
            {
                genders = await _context.Gender.ToListAsync();
                _cache.Set(ChacheKeys.Genders, genders);
            }

            return _mapper.Map<List<GenderDto>>(genders);
        }
    }
}
