using AutoMapper;
using EmployeesAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Models.Dtos.Positions
{
    [AutoMap(typeof(Position))]
    public class PositionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
