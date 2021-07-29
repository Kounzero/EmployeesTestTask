using AutoMapper;

using EmployeesAPI.Models.Dtos.Employees;
using EmployeesAPI.Models.Dtos.Genders;
using EmployeesAPI.Models.Dtos.Positions;
using EmployeesAPI.Models.Dtos.Subdivisions;
using EmployeesAPI.Models.Entities;
using System.Linq;

namespace EmployeesAPI.Mappings
{
    /// <summary>
    /// Профили для преобразования моделей данных
    /// </summary>
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
