using EmployeesAPI.Models.Dtos.Positions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    public interface IPositionService
    {
        public Task<IEnumerable<PositionDto>> GetPositions();
    }
}
