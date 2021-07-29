using EmployeesAPI.Entities;
using EmployeesAPI.Models.Dtos.Subdivisions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Services
{
    public interface ISubdivisionService
    {
        public Task<List<SubdivisionDto>> GetAllSubdivisions();
        public Task<List<SubdivisionDto>> GetSubdivisions(int? parentSubdivisionId);
        public Task<int> EditSubdivision(EditSubdivisionDto editSubdivisionDto);
        public Task<int> AddSubdivision(AddSubdivisionDto addSubdivisionDto);
        public Task<int> DeleteSubdivision(int id);
        public Task<bool> CheckSubdivisionParentingPossibility(int subdivisionId, int targetSubdivisionId);
        public Task<List<SubdivisionDto>> GetAllSubdivisionChildren(int subdivisionId);
    }
}
