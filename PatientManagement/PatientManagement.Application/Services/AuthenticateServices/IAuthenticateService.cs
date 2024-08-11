using System;
using PatientManagement.Application.DTOs;

namespace PatientManagement.Application.Services.AuthenticateServices
{
	public interface IAuthenticateService
	{
		Task<TokenDTO> Login(LoginDTO login);
	}
}

