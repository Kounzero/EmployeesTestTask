using System;

namespace EmployeesClient.Models.Employees
{
    /// <summary>
    /// Модель для работы с данными о сотруднике при изменении
    /// </summary>
    public class EditEmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public int PositionId { get; set; }
        public bool HasDrivingLicense { get; set; }
        public int SubdivisionId { get; set; }
    }
}
