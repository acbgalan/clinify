using Medo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinify.Data.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; } = Uuid7.NewUuid7();
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int SpecialtyId { get; set; }
        public string ResumeExtract { get; set; } = string.Empty;
        public string ResumeExperience { get; set; } = string.Empty;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public required Specialty Specialty { get; set; }
    }
}