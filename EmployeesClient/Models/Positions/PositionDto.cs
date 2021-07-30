namespace EmployeesClient.Models.Positions
{
    /// <summary>
    /// Модель для работы с данными о должностях
    /// </summary>
    public class PositionDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Title { get; set; }
    }
}
