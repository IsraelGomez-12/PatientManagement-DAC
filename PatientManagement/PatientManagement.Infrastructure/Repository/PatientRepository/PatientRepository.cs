using System;
using Dapper;
using PatientManagement.Domain.Models;

namespace PatientManagement.Infrastructure.Repository.PatientRepository
{
	public class PatientRepository : RepositoryBase , IPatientRepository
	{

		public PatientRepository(string connectionString) : base(connectionString)
		{

		}

        public async Task AddPatientAsync(Patient patient)
        {
            using (var connection = await GetConnectionAsync())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var query = "INSERT INTO Patient (FirstName, LastName, DateOfBirth, Email, Phone, Address, SocialSecurityNumber, IDNumber,Comments) " +
                                "VALUES (@FirstName, @LastName, @DateOfBirth, @Email, @Phone, @Address, @SocialSecurityNumber, @IDNumber,@Comments)";
                    await connection.ExecuteAsync(query, patient, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error adding patients.", ex);
                }
            }
        }

        public async Task DeletePatientAsync(int id)
        {
            using (var connection = await GetConnectionAsync())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await connection.ExecuteAsync("DELETE FROM Patient WHERE PatientId = @Id", new { Id = id },transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error deleting patients.", ex);
                }
            }
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            using (var connection = await GetConnectionAsync())
            {
                try
                {
                    return await connection.QuerySingleOrDefaultAsync<Patient>("SELECT * FROM Patient WHERE PatientId = @Id", new { Id = id });

                }
                catch(Exception ex)
                {
                    throw new Exception("Error retrieving patients by id.", ex);
                }
            }
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            using (var connection = await GetConnectionAsync())
            {
                try
                {
                    return await connection.QueryAsync<Patient>("SELECT * FROM Patient");

                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving patients.", ex);
                }
            }
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            using (var connection = await GetConnectionAsync())
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var query = "UPDATE Patient SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, " +
                                "Email = @Email, Phone = @Phone, Address = @Address, SocialSecurityNumber = @SocialSecurityNumber, " +
                                "IDNumber = @IDNumber, Comments = @Comments WHERE PatientId = @PatientId";
                    await connection.ExecuteAsync(query, patient, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error updating patients.", ex);
                }
            }
        }
    }
}

