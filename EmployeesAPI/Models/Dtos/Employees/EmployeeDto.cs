using AutoMapper;
using EmployeesAPI.Models.Entities;
using System;

namespace EmployeesAPI.Models.Dtos.Employees
{
    /// <summary>
    /// Объект передачи данных о сотруднике
    /// </summary>
    [AutoMap(typeof(Employee))]
    public class EmployeeDto
    {
        ///<inheritdoc cref="Employee.Id"/>
        public int Id { get; set; }

        ///<inheritdoc cref="Employee.FullName"/>
        public string FullName { get; set; }

        ///<inheritdoc cref="Employee.BirthDate"/>
        public DateTime BirthDate { get; set; }

        ///<inheritdoc cref="Employee.GenderId"/>
        public int GenderId { get; set; }

        /// <summary>
        /// Наименование пола
        /// </summary>
        public string GenderTitle { get; set; }

        ///<inheritdoc cref="Employee.PositionId"/>
        public int PositionId { get; set; }

        /// <summary>
        /// Наименование должности
        /// </summary>
        public string PositionTitle { get; set; }

        ///<inheritdoc cref="Employee.HasDrivingLicense"/>
        public bool HasDrivingLicense { get; set; }

        ///<inheritdoc cref="Employee.SubdivisionId"/>
        public int SubdivisionId { get; set; }

        /// <summary>
        /// Наименование подразделения
        /// </summary>
        public string SubdivisionTitle { get; set; }
    }
}
