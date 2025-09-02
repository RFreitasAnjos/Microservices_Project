using Educational_Victoria.Models;

namespace Educational_Victoria.DTOs.UsersDto
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EnumRoles Role { get; set; }
    }
}
