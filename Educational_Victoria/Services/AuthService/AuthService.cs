using Educational_Victoria.DTOs.LoginDto;
using Educational_Victoria.Interfaces.IRepositories;
using Educational_Victoria.Interfaces.IServices;
using Educational_Victoria.Models;
using Educational_Victoria.Repositories.AuthRepository;

namespace Educational_Victoria.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public AuthService(AuthRepository authRepository, IUserRepository userRepository, IJwtService jwtService)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new Exception("Email ou senha inválidos");

            var token = _jwtService.GenerateToken(user.UserId, user.Email, user.Role);

            return new LoginResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role
            };
        }

    }
}