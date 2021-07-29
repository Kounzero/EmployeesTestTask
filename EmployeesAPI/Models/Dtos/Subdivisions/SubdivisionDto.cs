using AutoMapper;
using EmployeesAPI.Models.Entities;
using System;

namespace EmployeesAPI.Models.Dtos.Subdivisions
{
    /// <summary>
    /// Объект передачи данных о подразделениях
    /// </summary>
    [AutoMap(typeof(Subdivision))]
    public class SubdivisionDto
    {
        /// <inheritdoc cref="Subdivision.Id"/>
        public int Id { get; set; }

        /// <inheritdoc cref="Subdivision.Title"/>
        public string Title { get; set; }

        /// <inheritdoc cref="Subdivision.FormDate"/>
        public DateTime FormDate { get; set; }

        /// <inheritdoc cref="Subdivision.Description"/>
        public string Description { get; set; }

        /// <inheritdoc cref="Subdivision.ParentId"/>
        public int? ParentId { get; set; }

        /// <summary>
        /// Наличие дочерних подразделений
        /// </summary>
        public bool HasChildren { get; set; }
    }
}
