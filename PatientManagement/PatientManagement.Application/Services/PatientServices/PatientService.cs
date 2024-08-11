using System;
using AutoMapper;
using PatientManagement.Application.DTOs;
using PatientManagement.Domain.Models;
using PatientManagement.Infrastructure.Repository.PatientRepository;

namespace PatientManagement.Application.Services.PatientServices
{
	public class PatientService : IPatientService
	{
        private readonly IPatientRepository _repository;
        private readonly IMapper _mapper;

		public PatientService(IPatientRepository repository, IMapper mapper)
		{
            _repository = repository;
            _mapper = mapper;
		}

        public async Task AddPatientAsync(PatientDTO patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            await _repository.AddPatientAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _repository.DeletePatientAsync(id);
        }

        public async Task<PatientDTO> GetPatientByIdAsync(int id)
        {
            var patient = await _repository.GetPatientByIdAsync(id);
            return _mapper.Map<PatientDTO>(patient);
        }

        public async Task<IEnumerable<PatientDTO>> GetPatientsAsync()
        {
            var patient = await _repository.GetPatientsAsync();
            return _mapper.Map<IEnumerable<PatientDTO>>(patient);
        }

        public async Task UpdatePatientAsync(PatientDTO patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            await _repository.UpdatePatientAsync(patient);
        }
    }
}

