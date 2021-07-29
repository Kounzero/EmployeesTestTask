namespace EmployeesClient.Models.Subdivisions
{
    public class AddSubdivisionDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentSubdivisionId { get; set; }
    }
}
