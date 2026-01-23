using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinify.Data.Entities
{
    public class Specialty
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}