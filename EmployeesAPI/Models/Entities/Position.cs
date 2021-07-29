namespace EmployeesAPI.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Должность
    /// </summary>
    [Table("Position")]
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
