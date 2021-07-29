namespace EmployeesAPI.Models.Dtos.Subdivisions
{
    /// <summary>
    /// Объект передачи данных при добавлении подразделения
    /// </summary>
    public class AddSubdivisionDto
    {
        /// <inheritdoc cref="Entities.Subdivision.Id"/>
        public string Title { get; set; }

        /// <inheritdoc cref="Entities.Subdivision.Description"/>
        public string Description { get; set; }

        /// <inheritdoc cref="Entities.Subdivision.ParentId"/>
        public int? ParentId { get; set; }
    }
}
