namespace EmployeesAPI.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// ���������
    /// </summary>
    [Table("Employee")]
    public partial class Employee
    {
        /// <summary>
        /// �������������
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        /// <summary>
        /// ���� ��������
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// ������������� ����
        /// </summary>
        public int GenderId { get; set; }

        /// <summary>
        /// ������������� ���������
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// ������� ������������ ����
        /// </summary>
        public bool HasDrivingLicense { get; set; }

        /// <summary>
        /// ������������� �������������
        /// </summary>
        public int SubdivisionId { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public virtual Gender Gender { get; set; }

        /// <summary>
        /// ���������
        /// </summary>
        public virtual Position Position { get; set; }

        /// <summary>
        /// �������������
        /// </summary>
        public virtual Subdivision Subdivision { get; set; }
    }
}
