using AutoMapper;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using EmployeesAPI.Models.Dtos.Positions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public PositionService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PositionDto>> GetPositions()
        {
            return _mapper.Map<List<PositionDto>>(await _context.Position.ToListAsync());
        }
    }
}
