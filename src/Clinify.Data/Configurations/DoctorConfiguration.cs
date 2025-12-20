using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clinify.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clinify.Data.Configurations
{
    internal class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.SpecialtyId)
                .IsRequired();

            builder.Property(x => x.ResumeExtract)
                .HasMaxLength(5000);

            builder.Property(x => x.ResumeExperience)
                .HasMaxLength(5000);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.CreatedBy)
                .IsRequired();

            builder.Property(x => x.UpdatedBy)
                .IsRequired(false);

            builder.HasOne(x => x.Specialty)
                .WithMany(x => x.Doctors)
                .HasForeignKey(x => x.SpecialtyId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
