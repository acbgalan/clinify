using AutoMapper;
using Clinify.Data.Entities;
using Clinify.Data.Repositories;
using Clinify.Shared.Common;
using Clinify.Shared.Patient.Input;
using Clinify.Shared.Patient.Ouput;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Clinify.Server.Services.PatientService
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<PatientResponse>> GetPatientAsync(Guid id)
        {
            ServiceResult<PatientResponse> serviceResult;

            try
            {
                var patient = await _patientRepository.GetAsync(id);
                var patientResponse = patient != null ? _mapper.Map<PatientResponse>(patient) : null;

                serviceResult = new ServiceResult<PatientResponse>()
                {
                    Data = patientResponse,
                    Success = patient != null,
                    Message = patient != null ? "Patient retrieved" : "Patient not found",
                    StatusCode = patient != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound
                };
            }
            catch (DbUpdateException ex)
            {
                serviceResult = new ServiceResult<PatientResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = $"Database error: {ex.Message}",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            catch (Exception ex)
            {
                serviceResult = new ServiceResult<PatientResponse>()
                {
                    Data = null,
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<PatientResponse>>> GetPatientsAsync()
        {
            ServiceResult<List<PatientResponse>> serviceResult;

            try
            {
                var patients = await _patientRepository.GetAllAsync();
                var patiensResponse = _mapper.Map<List<PatientResponse>>(patients);

                serviceResult = new ServiceResult<List<PatientResponse>>()
                {
                    Data = patiensResponse,
                    Success = true,
                    Message = "Patients retrieved",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            catch (Exception ex)
            {
                serviceResult = new ServiceResult<List<PatientResponse>>()
                {
                    Data = null,
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return serviceResult;
        }

        public async Task<ServiceResult<Patient>> CreatePatientAsync(CreatePatientRequest createPatientRequest)
        {
            ServiceResult<Patient> serviceResult;

            try
            {
                //TODO: Recuperar usuario y validar que se ha podido recuperar. Usuario necesario para asignar a CreatedBy
                var patient = _mapper.Map<Patient>(createPatientRequest);
                //TODO: CreatedBy debe tener usuario
                await _patientRepository.AddAsync(patient);
                int saveResult = await _patientRepository.SaveAsync();

                serviceResult = new ServiceResult<Patient>()
                {
                    Data = patient,
                    Success = saveResult > 0,
                    Message = saveResult > 0 ? "Patient created successfully" : "Unexpected value when saving",
                    StatusCode = saveResult > 0 ? StatusCodes.Status201Created : StatusCodes.Status500InternalServerError
                };
            }
            catch (DbUpdateException ex)
            {
                serviceResult = new ServiceResult<Patient>()
                {
                    Data = null,
                    Success = false,
                    Message = $"Database error: {ex.Message}",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
            catch (Exception ex)
            {
                serviceResult = new ServiceResult<Patient>()
                {
                    Data = null,
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}",
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return serviceResult;
        }

    }
}
