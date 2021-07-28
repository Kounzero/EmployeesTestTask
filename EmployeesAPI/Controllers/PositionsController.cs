using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesAPI.Entities;
using EmployeesAPI.Models;
using AutoMapper;
using EmployeesAPI.Models.Dtos.Positions;
using EmployeesAPI.Services;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private IPositionService PositionService;

        public PositionsController(IPositionService positionService)
        {
            PositionService = positionService;
        }

        // GET: api/Positions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionDto>>> GetPositions()
        {
            return Ok(await PositionService.GetPositions());
        }
    }
}
