using AutoMapper;
using AutoMapper.Configuration.Annotations;
using EmployeesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Models.Dtos.Employees
{
    [AutoMap(typeof(Employee))]
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public string GenderTitle { get; set; }
        public int PositionId { get; set; }
        public string PositionTitle { get; set; }
        public bool HasDrivingLicense { get; set; }
        public int SubdivisionId { get; set; }
        public string SubdivisionTitle { get; set; }
    }
}
