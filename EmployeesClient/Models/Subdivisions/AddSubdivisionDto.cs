namespace EmployeesClient.Models.Subdivisions
{
    /// <summary>
    /// Модель для работы с данными о подразделении при добавлении
    /// </summary>
    public class AddSubdivisionDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ParentSubdivisionId { get; set; }
    }
}
