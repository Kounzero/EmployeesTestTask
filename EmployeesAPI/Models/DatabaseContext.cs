using EmployeesAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EmployeesAPI.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Subdivision> Subdivision { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasIndex(x => x.Id)
                    .IsUnique();
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasIndex(x => x.Id)
                    .IsUnique();
            });

            modelBuilder.Entity<Subdivision>(entity =>
            {
                entity.HasIndex(x => x.Id)
                    .IsUnique();
            });

            modelBuilder.Entity<Subdivision>(entity =>
            {
                entity.HasIndex(x => x.Id)
                    .IsUnique();
            });
        }
    }
}
