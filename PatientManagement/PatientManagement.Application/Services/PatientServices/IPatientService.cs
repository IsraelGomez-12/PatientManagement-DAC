using System;
using PatientManagement.Application.DTOs;

namespace PatientManagement.Application.Services.PatientServices
{
	public interface IPatientService
	{
        Task<IEnumerable<PatientDTO>> GetPatientsAsync();
        Task<PatientDTO> GetPatientByIdAsync(int id);
        Task AddPatientAsync(PatientDTO patientDto);
        Task UpdatePatientAsync(PatientDTO patientDto);
        Task DeletePatientAsync(int id);
    }
}

