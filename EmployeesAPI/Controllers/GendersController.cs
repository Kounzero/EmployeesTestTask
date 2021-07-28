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
using EmployeesAPI.Models.Dtos.Genders;
using EmployeesAPI.Services;

namespace EmployeesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private IGenderService GenderService;

        public GendersController(IGenderService genderService)
        {
            GenderService = genderService;
        }

        // Get all genders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenderDto>>> GetGenders()
        {
            return Ok(await GenderService.GetGenders());
        }
    }
}
