using System;
using PatientManagement.Domain.Models;

namespace PatientManagement.Infrastructure.Repository.PatientRepository
{
	public interface IPatientRepository
	{
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task AddPatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
    }
}

