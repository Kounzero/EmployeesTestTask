using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesClient.Models.Employees
{
    public class EmployeeDto
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderID { get; set; }
        public string GenderTitle { get; set; }
        public int PositionID { get; set; }
        public string PositionTitle { get; set; }
        public bool HasDrivingLicense { get; set; }
        public int SubdivisionID { get; set; }
        public string SubdivisionTitle { get; set; }

        public string ShowedHasDrivingLicense { get { return HasDrivingLicense ? "Да" : "Нет"; } }
        public string ShowedDate { get { return BirthDate.ToShortDateString(); } }

    }
}
