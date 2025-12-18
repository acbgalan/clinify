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
            try
            {
                var patient = await _patientRepository.GetAsync(id);
                var patientResponse = patient != null ? _mapper.Map<PatientResponse>(patient) : null;

                if (patient != null)
                {
                    return SuccessResult<PatientResponse>(patientResponse!, "Patient retrieved successfully", StatusCodes.Status200OK);
                }
                else
                {
                    return FailureResult<PatientResponse>("Patient not found", StatusCodes.Status404NotFound);
                }
            }
            catch (DbUpdateException ex)
            {
                return HandleDbUpdateException<PatientResponse>(ex);
            }
            catch (Exception ex)
            {
                return HandleGeneralException<PatientResponse>(ex);
            }
        }

        public async Task<ServiceResult<List<PatientResponse>>> GetPatientsAsync()
        {
            try
            {
                var patients = await _patientRepository.GetAllAsync();
                var patiensResponse = _mapper.Map<List<PatientResponse>>(patients);
                return SuccessResult<List<PatientResponse>>(patiensResponse, "Patients retrieved successfully", StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return HandleGeneralException<List<PatientResponse>>(ex);
            }
        }

        public async Task<ServiceResult<PatientResponse>> CreatePatientAsync(CreatePatientRequest createPatientRequest)
        {
            try
            {
                //TODO: Recuperar usuario y validar que se ha podido recuperar. Usuario necesario para asignar a CreatedBy
                var patient = _mapper.Map<Patient>(createPatientRequest);
                //TODO: CreatedBy debe tener usuario
                await _patientRepository.AddAsync(patient);
                int saveResult = await _patientRepository.SaveAsync();

                if (saveResult > 0)
                {
                    var patientResponse = _mapper.Map<PatientResponse>(patient);
                    return SuccessResult<PatientResponse>(patientResponse, "Patient created successfully", StatusCodes.Status201Created);
                }
                else
                {
                    return FailureResult<PatientResponse>("Unexpected value when creating a new patient", StatusCodes.Status500InternalServerError);
                }
            }
            catch (DbUpdateException ex)
            {
                return HandleDbUpdateException<PatientResponse>(ex);
            }
            catch (Exception ex)
            {
                return HandleGeneralException<PatientResponse>(ex);
            }
        }

        public async Task<ServiceResult> UpdatePatient(UpdatePatientRequest updatePatientRequest)
        {
            try
            {
                var patient = _mapper.Map<Patient>(updatePatientRequest);
                await _patientRepository.UpdateAsync(patient);
                int saveResult = await _patientRepository.SaveAsync();

                if (saveResult > 0)
                {
                    return SuccessResult("Patient updated successfully", StatusCodes.Status204NoContent);
                }
                else
                {
                    return FailureResult("Unexpected value when updating a new patient", StatusCodes.Status500InternalServerError);
                }
            }
            catch (DbUpdateException ex)
            {
                return HandleDbUpdateException(ex);
            }
            catch (Exception ex)
            {
                return HandleGeneralException(ex);
            }
        }

        public async Task<ServiceResult> DeletePatient(Guid id)
        {
            try
            {
                var patient = await _patientRepository.GetAsync(id);

                if (patient == null)
                {
                    return SuccessResult("Patient not found", StatusCodes.Status404NotFound);
                }
                else
                {
                    await _patientRepository.DeleteAsync(patient);
                    int saveResult = await _patientRepository.SaveAsync();

                    if (saveResult > 0)
                    {
                        return SuccessResult("Patient removed successfully", StatusCodes.Status204NoContent);
                    }
                    else
                    {
                        return FailureResult("Unexpected value when deleting patient", StatusCodes.Status500InternalServerError);
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                return HandleDbUpdateException(ex);
            }
            catch (Exception ex)
            {
                return HandleGeneralException(ex);
            }
        }


        #region Auxiliary methods
        private ServiceResult<T> SuccessResult<T>(T data, string message, int statusCode)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Success = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        private ServiceResult SuccessResult(string message, int statusCode)
        {
            return new ServiceResult
            {
                Success = true,
                Message = message,
                StatusCode = statusCode
            };
        }

        private ServiceResult<T> FailureResult<T>(string message, int statusCode, T? data = default)
        {
            return new ServiceResult<T>
            {
                Data = data,
                Success = false,
                Message = message,
                StatusCode = statusCode
            };
        }

        private ServiceResult FailureResult(string message, int statusCode)
        {
            return new ServiceResult
            {
                Success = false,
                Message = message,
                StatusCode = statusCode
            };
        }

        private ServiceResult<T> HandleDbUpdateException<T>(DbUpdateException ex)
        {
            return FailureResult<T>($"Database error: {ex.Message}", StatusCodes.Status500InternalServerError);
        }

        private ServiceResult HandleDbUpdateException(DbUpdateException ex)
        {
            return FailureResult($"Database error: {ex.Message}", StatusCodes.Status500InternalServerError);
        }

        private ServiceResult<T> HandleGeneralException<T>(Exception ex)
        {
            return FailureResult<T>($"Unexpected error: {ex.Message}", StatusCodes.Status500InternalServerError);
        }

        private ServiceResult HandleGeneralException(Exception ex)
        {
            return FailureResult($"Unexpected error: {ex.Message}", StatusCodes.Status500InternalServerError);
        }

        #endregion
    }
}
