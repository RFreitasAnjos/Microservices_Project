using Educational_Victoria.Models;

namespace Educational_Victoria.Interfaces.IServices
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string email, EnumRoles role);
    }
}
