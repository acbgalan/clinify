using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clinify.Shared.Common.Enums;

namespace Clinify.Data.Entities
{
    public class Patient
    {
        public Guid Id { get; set; } 
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
    }
}
