using EmployeesAPI.Models.Dtos.Genders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    public interface IGenderService
    {
        public Task<IEnumerable<GenderDto>> GetGenders();
    }
}
