using Educational_Victoria.DTOs.UserSubjectAccess;
using Educational_Victoria.Models;

namespace Educational_Victoria.Interfaces.IServices
{
    public interface IUserSubjectAccessService
    {
        Task<UserSubjectAccessDto> GrantAccessAsync(int userId, int subjectId);
        Task<IEnumerable<UserSubjectAccessDto>> GetSubjectsByUserAsync(int userId);
        Task<IEnumerable<UserSubjectAccessDto>> GetUsersBySubjectAsync(int SubjectId);
        Task<bool> RevokeAccessAsync(int userId, int subjectId);
    }
}
