using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Clinify.Shared.Patient.Input
{
    public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequest>
    {
        public CreatePatientRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("The name is required and cannot be empty.")
                .Length(2, 50).WithMessage("The name must be between 2 and 50 characters long.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("The last name is required and cannot be empty.")
                .Length(2, 50).WithMessage("The last name must be between 2 and 50 characters long.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("The birthdate is required.")
                .Must(IsValidPastDate).WithMessage("The date of birth cannot be in the future.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("The provided gender value is not valid.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("The phone is required and cannot be empty.")
                .MaximumLength(20).WithMessage("The phone must not exceed 20 characters.")
                .Matches(@"^\+?[0-9\s-]{7,20}$").WithMessage("The phone format is not valid. It must contain only numbers, spaces, or hyphens.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("The email is required and cannot be empty.")
                .MaximumLength(200).WithMessage("The email must not exceed 20 characters.")
                .EmailAddress().WithMessage("El formato del correo electrónico no es válido.");
        }

        private bool IsValidPastDate(DateOnly birthdate)
        {
            return birthdate < DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
