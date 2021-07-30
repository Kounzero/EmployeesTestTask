using System;
using System.Windows;

namespace EmployeesClient.Models.Subdivisions
{
    /// <summary>
    /// 
    /// </summary>
    public class SubdivisionDto
    {
        public SubdivisionDto()
        {
            Opening = false;
            LeftMargin = 0;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime FormDate { get; set; }
        public string Description { get; set; }
        public int? ParentSubdivisionID { get; set; }
        public bool HasChildren { get; set; }

        public Visibility ShowedVisibility { get { return HasChildren ? Visibility.Visible : Visibility.Hidden; } }
        public string MarginForDisplay { get { return $"{LeftMargin} 0 0 0"; } }
        public int LeftMargin { get; set; }
        public bool Opening { get; set; }
        public string BtnDisplayContent { get { return Opening ? "v" : ">"; } }
    }
}
