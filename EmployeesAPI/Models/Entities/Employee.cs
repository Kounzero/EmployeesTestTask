namespace EmployeesAPI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Employee")]
    public partial class Employee
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FullName { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        public int GenderID { get; set; }

        public int PositionID { get; set; }

        public bool HasDrivingLicense { get; set; }

        public int SubdivisionId { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Position Position { get; set; }

        public virtual Subdivision Subdivision { get; set; }
    }
}
