using Educational_Victoria.DTOs.UserSubjectAccess;

namespace Educational_Victoria.DTOs.UsersDto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public List<UserSubjectAccessDto> UserAccesses { get; set; }
    }
}
