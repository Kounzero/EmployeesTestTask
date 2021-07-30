using System;

namespace EmployeesClient.Models.Employees
{
    /// <summary>
    /// Модель для работы с данными о сотруднике при добавлении нового
    /// </summary>
    public class AddEmployeeDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public int PositionId { get; set; }
        public bool HasDrivingLicense { get; set; }
        public int SubdivisionId { get; set; }
    }
}
