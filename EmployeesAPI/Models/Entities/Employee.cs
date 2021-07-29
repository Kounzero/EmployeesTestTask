namespace EmployeesAPI.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Сотрудник
    /// </summary>
    [Table("Employee")]
    public partial class Employee
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Идентификатор пола
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// Идентификатор должности
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Наличие водительских прав
        /// </summary>
        public bool HasDrivingLicense { get; set; }

        /// <summary>
        /// Идентификатор подразделения
        /// </summary>
        public int SubdivisionId { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public virtual Gender Gender { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public virtual Position Position { get; set; }

        /// <summary>
        /// Подразделение
        /// </summary>
        public virtual Subdivision Subdivision { get; set; }
    }
}
