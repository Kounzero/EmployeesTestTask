namespace EmployeesClient.Models.Subdivisions
{
    public class EditSubdivisionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentSubdivisionId { get; set; }
    }
}
