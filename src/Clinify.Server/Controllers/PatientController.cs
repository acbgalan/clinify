using AutoMapper;
using Clinify.Data.Entities;
using Clinify.Data.Repositories;
using Clinify.Server.Services.PatientService;
using Clinify.Shared.Patient.Input;
using Clinify.Shared.Patient.Ouput;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinify.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IValidator<CreatePatientRequest> _validator;
        private readonly IPatientService _patientService;

        public PatientController(IValidator<CreatePatientRequest> validator, IPatientService patientService)
        {
            _validator = validator;
            _patientService = patientService;
        }

        [HttpGet("{id:Guid}", Name = "GetPatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PatientResponse>> GetPatient(Guid id)
        {
            //TODO: Puede llegar un Id null, revisar?
            var serviceResult = await _patientService.GetPatientAsync(id);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.Message);
            }

            return Ok(serviceResult.Data);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<PatientResponse>>> GetAllPatients()
        {
            var serviceResult = await _patientService.GetPatientsAsync();

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.Message);
            }

            return Ok(serviceResult.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreatePatient([FromBody] CreatePatientRequest createPatientRequest)
        {
            //TODO: Si es null el validator falla?
            var validationResult = await _validator.ValidateAsync(createPatientRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var serviceResult = await _patientService.CreatePatientAsync(createPatientRequest);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.Message);
            }

            return CreatedAtRoute("GetPatient", new { id = serviceResult.Data!.Id }, serviceResult.Data);
        }


    }
}
