using Clinify.Shared.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinify.Shared.Patient.Input
{
    public class UpdatePatientRequest
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
    }
}
