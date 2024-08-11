using System;
namespace PatientManagement.Application.Services.JWTServices
{
	public interface IJwtService
	{
        string GenerateToken(string username);
    }
}

