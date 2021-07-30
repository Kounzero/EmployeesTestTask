namespace EmployeesClient.Models.Subdivisions
{
    /// <summary>
    /// Модель для работы с данными о подразделении при добавлении
    /// </summary>
    public class AddSubdivisionDto
    {
        /// <inheritdoc cref="SubdivisionDto.Title"/>
        public string Title { get; set; }

        /// <inheritdoc cref="SubdivisionDto.Description"/>
        public string Description { get; set; }

        /// <inheritdoc cref="SubdivisionDto.ParentId"/>
        public int? ParentId { get; set; }
    }
}
