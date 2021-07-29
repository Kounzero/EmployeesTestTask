namespace EmployeesClient.Models.Subdivisions
{
    public class EditSubdivisionDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentSubdivisionID { get; set; }
    }
}
