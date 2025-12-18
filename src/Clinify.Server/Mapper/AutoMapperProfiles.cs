using AutoMapper;
using Clinify.Data.Entities;
using Clinify.Shared.Patient.Input;
using Clinify.Shared.Patient.Ouput;

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
            CreateMap<Patient, PatientResponse>().ReverseMap();
            CreateMap<UpdatePatientRequest, Patient>();
        }
    }
}
