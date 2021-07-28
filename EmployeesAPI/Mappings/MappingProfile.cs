using AutoMapper;
using EmployeesAPI.Entities;
using EmployeesAPI.Models.Dtos.Employees;
using EmployeesAPI.Models.Dtos.Genders;
using EmployeesAPI.Models.Dtos.Positions;
using EmployeesAPI.Models.Dtos.Subdivisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Gender, GenderDto>();
            CreateMap<Position, PositionDto>();
            CreateMap<Subdivision, SubdivisionDto>()
                .ForMember(src => src.HasChildren, opt => opt.MapFrom(c => c.Subdivisions.Any()));
        }
    }
}
