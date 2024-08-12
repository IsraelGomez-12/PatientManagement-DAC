using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Application.DTOs;
using PatientManagement.Application.Helpers.EncryptionServices;
using PatientManagement.Application.Services.PatientServices;

namespace PatientManagement.API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;
    private readonly IAesEncryptionService _aes;

    public PatientController(IPatientService patientService, IAesEncryptionService aes)
    {
        _patientService = patientService;
        _aes = aes;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetPatients()
    {
        try
        {
            var patients = await _patientService.GetPatientsAsync();
            return Ok(patients);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPatient(int id)
    {
        try
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound($"Patient with ID:{id} was not found");
            }

            //DesEncrypting SSN
            patient.SocialSecurityNumber = _aes.Decrypt(patient.SocialSecurityNumber);

            return Ok(patient);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostPatient([FromBody] PatientDTO patientDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Encrypting SSN
            patientDto.SocialSecurityNumber = _aes.Encrypt(patientDto.SocialSecurityNumber);

            await _patientService.AddPatientAsync(patientDto);
            return CreatedAtAction(nameof(GetPatient), new { id = patientDto.PatientId }, patientDto);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutPatient(int id,[FromBody] PatientDTO patientDto)
    {
        try
        {
            if (id != patientDto.PatientId)
            {
                return BadRequest();
            }

            var patientFound = await _patientService.GetPatientByIdAsync(id);
            if (patientFound == null)
            {
                return NotFound($"Patient with ID:{id} was not found");
            }

            //Encrypting SSN
            patientDto.SocialSecurityNumber = _aes.Encrypt(patientDto.SocialSecurityNumber);

            await _patientService.UpdatePatientAsync(patientDto);
            return Ok("Patient successfully updated");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletePatient(int id)
    {
        try
        {
            var patientFound = await _patientService.GetPatientByIdAsync(id);
            if (patientFound == null)
            {
                return NotFound($"Patient with ID:{id} was not found");
            }

            await _patientService.DeletePatientAsync(id);
            return Ok("Patient successfully deleted");
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }

    }
}

