using AutoMapper;
using EmployeesAPI.Models.Entities;

namespace EmployeesAPI.Models.Dtos.Positions
{
    /// <summary>
    /// Объект передачи данных о должностях
    /// </summary>
    [AutoMap(typeof(Position))]
    public class PositionDto
    {
        /// <inheritdoc cref="Position.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="Position.Title"/>
        public string Title { get; set; }
    }
}
