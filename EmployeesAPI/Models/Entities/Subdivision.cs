namespace EmployeesAPI.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Подразделение
    /// </summary>
    [Table("Subdivision")]
    public partial class Subdivision
    {
        public Subdivision()
        {
            Employees = new HashSet<Employee>();
            Subdivisions = new HashSet<Subdivision>();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// Дата формирования
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime FormDate { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор родительноского подразделения
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }

        /// <summary>
        /// Подразделения
        /// </summary>
        public virtual ICollection<Subdivision> Subdivisions { get; set; }

        /// <summary>
        /// Родитель
        /// </summary>
        public virtual Subdivision Parent { get; set; }
    }
}
