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
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("{id:Guid}", Name = "GetPatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PatientResponse>> GetPatient(Guid id)
        {
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
            var serviceResult = await _patientService.CreatePatientAsync(createPatientRequest);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.Message);
            }

            return CreatedAtRoute("GetPatient", new { id = serviceResult.Data!.Id }, serviceResult.Data);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePatient(Guid id, UpdatePatientRequest updatePatientRequest)
        {
            if (id != updatePatientRequest.Id)
            {
                return BadRequest("Id mismatch");
            }

            var serviceResult = await _patientService.UpdatePatient(updatePatientRequest);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePatient(Guid id)
        {
            var serviceResult = await _patientService.DeletePatient(id);

            if (!serviceResult.Success)
            {
                return StatusCode(serviceResult.StatusCode, serviceResult.Message);
            }

            return NoContent();
        }
    }
}
