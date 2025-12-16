using Clinify.Data.Entities;
using Clinify.Shared.Common;
using Clinify.Shared.Patient.Input;
using Clinify.Shared.Patient.Ouput;
using Microsoft.AspNetCore.Mvc;

namespace Clinify.Server.Services.PatientService
{
    public interface IPatientService
    {
        Task<ServiceResult<PatientResponse>> GetPatient(Guid id);
        Task<ServiceResult<Patient>> CreatePatientAsync(CreatePatientRequest createPatientRequest);
    }
}
