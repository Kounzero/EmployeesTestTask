using System;

namespace EmployeesClient.Models.Employees
{
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

        public string ShowedHasDrivingLicense { get { return HasDrivingLicense ? "Да" : "Нет"; } }
        public string ShowedDate { get { return BirthDate.ToShortDateString(); } }

    }
}
