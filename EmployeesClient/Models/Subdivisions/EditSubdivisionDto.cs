namespace EmployeesClient.Models.Subdivisions
{
    /// <summary>
    /// Модель для работы с данными о подразделении при измении
    /// </summary>
    public class EditSubdivisionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentSubdivisionId { get; set; }
    }
}
