using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesClient.Models.Employees
{
    public class AddEmployeeDto
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderID { get; set; }
        public int PositionID { get; set; }
        public bool HasDrivingLicense { get; set; }
        public int SubdivisionID { get; set; }
    }
}
