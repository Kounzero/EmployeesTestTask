using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Models.Dtos.Subdivisions
{
    public class AddSubdivisionDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
    }
}
