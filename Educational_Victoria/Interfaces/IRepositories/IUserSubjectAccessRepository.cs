using Educational_Victoria.Models;

namespace Educational_Victoria.Interfaces.IRepositories
{
    public interface IUserSubjectAccessRepository
    {
        Task<UserSubjectAccess> GetByIdAsync(int Id);
        Task<IEnumerable<UserSubjectAccess>> GetByUserIdAsync(int userId);
        Task<IEnumerable<UserSubjectAccess>> GetBySubjectIdAsync(int subjectId);
        Task<UserSubjectAccess> AddAsync(UserSubjectAccess userSubjectAccess);
        Task<bool> RemoveAsync(int userId, int subjectId);
    }
}
