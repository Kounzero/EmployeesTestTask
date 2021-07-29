using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesClient.AppConfig
{
    public class AppConfig
    {
        public List<string> ConnectionStrings { get; set; }

        public string GetCurrentConnectionString()
        {
            return ConnectionStrings[0];
        }
    }
}
