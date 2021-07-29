using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesAPI.Caching
{
    public static class ChacheKeys
    {
        public static string AllSubdivisions { get { return "_AllSubdivisions"; } }
        public static string SubdivisionsByParent { get { return "_SubdivisionsByParent_"; } }
        public static string Genders { get { return "_Genders"; } }
        public static string Positions { get { return "_Positions"; } }
        public static string EmployeesBySubdivision { get { return "_EmployeesBySubdivision_"; } }

    }
}
