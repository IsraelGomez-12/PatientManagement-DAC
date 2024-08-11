using System;
using PatientManagement.Domain.Models;

namespace PatientManagement.Infrastructure.Repository.UserRepository
{
	public interface IUserRepository
	{
		Task<Users> GetUserAsync(string userName, string password);
	}
}

