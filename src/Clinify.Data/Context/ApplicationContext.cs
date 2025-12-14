using Clinify.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinify.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patients");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);

                entity.HasIndex(p => new { p.FirstName, p.LastName });

                entity.Property(p => p.BirthDate)
                .IsRequired()
                .HasColumnType("date");

                entity.Property(p => p.Gender)
                .IsRequired();

                entity.Property(p => p.Phone)
                .IsRequired()
                .HasMaxLength(20);

                entity.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(200);

                entity.HasIndex(p => p.Email)
                .IsUnique();

                entity.Property(p => p.CreatedAt)
                .IsRequired();

                entity.Property(p => p.UpdatedAt)
                .IsRequired(false);

                entity.Property(p => p.CreatedBy)
                .IsRequired();

                entity.Property(p => p.UpdatedBy)
                .IsRequired(false);
            });
        }

    }
}
