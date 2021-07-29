namespace EmployeesAPI.Models.Dtos.Subdivisions
{
    /// <summary>
    /// Объект передачи данных при изменении данных о подразделении
    /// </summary>
    public class EditSubdivisionDto
    {
        /// <inheritdoc cref="Entities.Subdivision.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="Entities.Subdivision.Title"/>
        public string Title { get; set; }

        /// <inheritdoc cref="Entities.Subdivision.Description"/>
        public string Description { get; set; }

        /// <inheritdoc cref="Entities.Subdivision.ParentId"/>
        public int? ParentId { get; set; }
    }
}
