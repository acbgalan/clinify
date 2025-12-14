using Clinify.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinify.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);

            builder.HasIndex(p => new { p.FirstName, p.LastName });

            builder.Property(p => p.BirthDate)
            .IsRequired()
            .HasColumnType("date");

            builder.Property(p => p.Gender)
            .IsRequired();

            builder.Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(20);

            builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(200);

            builder.HasIndex(p => p.Email)
            .IsUnique();

            builder.Property(p => p.CreatedAt)
            .IsRequired();

            builder.Property(p => p.UpdatedAt)
            .IsRequired(false);

            builder.Property(p => p.CreatedBy)
            .IsRequired();

            builder.Property(p => p.UpdatedBy)
            .IsRequired(false);
        }
    }
}
