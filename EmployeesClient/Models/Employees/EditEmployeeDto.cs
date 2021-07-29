using System;

namespace EmployeesClient.Models.Employees
{
    public class EditEmployeeDto
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderID { get; set; }
        public int PositionID { get; set; }
        public bool HasDrivingLicense { get; set; }
        public int SubdivisionID { get; set; }
    }
}
