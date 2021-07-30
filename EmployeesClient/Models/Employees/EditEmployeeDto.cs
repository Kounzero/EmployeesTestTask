using System;

namespace EmployeesClient.Models.Employees
{
    /// <summary>
    /// Модель для работы с данными о сотруднике при изменении
    /// </summary>
    public class EditEmployeeDto
    {
        /// <inheritdoc cref="EmployeeDto.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="EmployeeDto.FullName"/>
        public string FullName { get; set; }

        /// <inheritdoc cref="EmployeeDto.BirthDate"/>
        public DateTime BirthDate { get; set; }

        /// <inheritdoc cref="EmployeeDto.GenderId"/>
        public int GenderId { get; set; }

        /// <inheritdoc cref="EmployeeDto.PositionId"/>
        public int PositionId { get; set; }

        /// <inheritdoc cref="EmployeeDto.HasDrivingLicense"/>
        public bool HasDrivingLicense { get; set; }

        /// <inheritdoc cref="EmployeeDto.SubdivisionId"/>
        public int SubdivisionId { get; set; }
    }
}
