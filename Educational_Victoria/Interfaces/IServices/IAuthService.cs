using Educational_Victoria.DTOs.LoginDto;

namespace Educational_Victoria.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto request);
    }
}
