namespace EmployeesAPI.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// �������������
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
        /// �������������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ������������
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        /// <summary>
        /// ���� ������������
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime FormDate { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ������������� ��������������� �������������
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }

        /// <summary>
        /// �������������
        /// </summary>
        public virtual ICollection<Subdivision> Subdivisions { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public virtual Subdivision Parent { get; set; }
    }
}
