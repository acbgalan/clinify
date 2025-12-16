using AutoMapper;
using Clinify.Data.Entities;
using Clinify.Data.Repositories;
using Clinify.Shared.Patient.Input;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinify.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IValidator<CreatePatientRequest> _validator;
        private readonly IMapper _mapper;

        public PatientController(IPatientRepository patientRepository, IValidator<CreatePatientRequest> validator, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet("{id:int}", Name = "GetPatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Patient>> GetPatient(Guid id)
        {
            var patient = await _patientRepository.GetAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreatePatient([FromBody] CreatePatientRequest createPatientRequest)
        {
            var validationResult = await _validator.ValidateAsync(createPatientRequest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToDictionary());
            }

            var patient = _mapper.Map<Patient>(createPatientRequest);
            await _patientRepository.AddAsync(patient);
            int saveResult = await _patientRepository.SaveAsync();

            if (!(saveResult > 0))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }





    }
}
