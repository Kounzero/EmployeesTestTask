using AutoMapper;
using EmployeesAPI.Models.Entities;

namespace EmployeesAPI.Models.Dtos.Genders
{
    /// <summary>
    /// Объект передачи данных о полах
    /// </summary>
    [AutoMap(typeof(Gender))]
    public class GenderDto
    {
        /// <inheritdoc cref="Gender.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="Gender.Title"/>
        public string Title { get; set; }
    }
}
