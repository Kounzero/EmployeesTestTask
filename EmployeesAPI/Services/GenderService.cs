using AutoMapper;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Genders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public GenderService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenderDto>> GetGenders()
        {
            return _mapper.Map<List<GenderDto>>(await _context.Gender.ToListAsync());
        }
    }
}
