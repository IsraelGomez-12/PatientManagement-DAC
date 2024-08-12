using System;
using PatientManagement.Application.DTOs;
using PatientManagement.Application.Helpers.JWTServices;
using PatientManagement.Infrastructure.Repository.UserRepository;

namespace PatientManagement.Application.Services.AuthenticateServices
{
	public class AuthenticateService : IAuthenticateService
	{
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthenticateService(IJwtService jwtService, IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
        }

        public async Task<TokenDTO> Login(LoginDTO login)
        {
            TokenDTO tokenDTO = new TokenDTO();
            var user = await _userRepository.GetUserAsync(login.Username, login.Password);
            if (user != null)
            {
                var token = _jwtService.GenerateToken(user.Username);
                tokenDTO.Token = token;
            }

            return tokenDTO;
        }
    }
}

