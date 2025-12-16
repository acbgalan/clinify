using AutoMapper;
using Clinify.Data.Entities;
using Clinify.Shared.Patient.Input;

namespace Clinify.Server.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            PatientMapping();
        }

        private void PatientMapping()
        {
            CreateMap<CreatePatientRequest, Patient>();
        }
    }
}
