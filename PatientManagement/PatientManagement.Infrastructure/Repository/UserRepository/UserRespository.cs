using System;
using Dapper;
using PatientManagement.Domain.Models;

namespace PatientManagement.Infrastructure.Repository.UserRepository
{
	public class UserRespository : RepositoryBase , IUserRepository
	{
		public UserRespository(string connectionString) : base(connectionString)
		{
		}

        public async Task<Users> GetUserAsync(string userName, string password)
        {
            using (var connection = await GetConnectionAsync())
            {
                try
                {
                    var query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                    return await connection.QuerySingleOrDefaultAsync<Users>(query, new { Username = userName , Password = password });

                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving user.", ex);
                }
            }
        }
    }
}

