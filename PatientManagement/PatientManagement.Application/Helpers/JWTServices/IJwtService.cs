using System;
namespace PatientManagement.Application.Helpers.JWTServices
{
	public interface IJwtService
	{
        string GenerateToken(string username);
    }
}

