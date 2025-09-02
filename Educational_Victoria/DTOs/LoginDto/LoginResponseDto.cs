using Educational_Victoria.Models;

namespace Educational_Victoria.DTOs.LoginDto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public EnumRoles Role { get; set; }
    }
}
