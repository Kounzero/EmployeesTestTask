namespace EmployeesClient.Models.Subdivisions
{
    /// <summary>
    /// Модель для работы с данными о подразделении при измении
    /// </summary>
    public class EditSubdivisionDto
    {
        /// <inheritdoc cref="SubdivisionDto.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="SubdivisionDto.Title"/>
        public string Title { get; set; }

        /// <inheritdoc cref="SubdivisionDto.Description"/>
        public string Description { get; set; }

        /// <inheritdoc cref="SubdivisionDto.ParentId"/>
        public int? ParentId { get; set; }
    }
}
