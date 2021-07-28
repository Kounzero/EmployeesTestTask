namespace EmployeesAPI.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("Subdivision")]
    public partial class Subdivision
    {
        public Subdivision()
        {
            Employees = new HashSet<Employee>();
            Subdivisions = new HashSet<Subdivision>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        public DateTime FormDate { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Subdivision> Subdivisions { get; set; }

        public virtual Subdivision Parent { get; set; }
    }
}
