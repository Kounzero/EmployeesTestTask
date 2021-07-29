namespace EmployeesAPI.Models.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ���
    /// </summary>
    [Table("Gender")]
    public partial class Gender
    {
        public Gender()
        {
            Employees = new HashSet<Employee>();
        }

        /// <summary>
        /// �������������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ������������
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// ����������
        /// </summary>
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
