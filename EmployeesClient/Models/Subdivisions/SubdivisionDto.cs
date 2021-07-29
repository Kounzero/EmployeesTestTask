using System;
using System.Windows;

namespace EmployeesClient.Models.Subdivisions
{
    public class SubdivisionDto
    {
        public SubdivisionDto()
        {
            Opened = false;
        }

        public int id { get; set; }
        public string title { get; set; }
        public DateTime formDate { get; set; }
        public string description { get; set; }
        public int? parentSubdivisionID { get; set; }
        public bool hasChildren { get; set; }

        public Visibility ShowedVisibility { get { return hasChildren ? Visibility.Visible : Visibility.Hidden; } }
        public string MarginForDisplay { get { return $"{LeftMargin} 0 0 0"; } }
        public int LeftMargin { get; set; }
        public bool Opened { get; set; }
        public string BtnDisplayContent { get { return Opened ? "v" : ">"; } }
    }
}
