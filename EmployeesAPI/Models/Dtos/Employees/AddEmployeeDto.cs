using AutoMapper;
using EmployeesAPI.Models.Entities;
using System;

namespace EmployeesAPI.Models.Dtos.Employees
{
    /// <summary>
    /// Объект передачи данных при добавлении сотрудника
    /// </summary>
    [AutoMap(typeof(Employee))]
    public class AddEmployeeDto
    {
        ///<inheritdoc cref="Employee.FullName"/>
        public string FullName { get; set; }

        ///<inheritdoc cref="Employee.BirthDate"/>
        public DateTime BirthDate { get; set; }

        ///<inheritdoc cref="Employee.GenderId"/>
        public int GenderId { get; set; }

        ///<inheritdoc cref="Employee.PositionId"/>
        public int PositionId { get; set; }

        ///<inheritdoc cref="Employee.HasDrivingLicense"/>
        public bool HasDrivingLicense { get; set; }

        ///<inheritdoc cref="Employee.SubdivisionId"/>
        public int SubdivisionId { get; set; }
    }
}
