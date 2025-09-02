using Educational_Victoria.Data;
using Educational_Victoria.Interfaces.IRepositories;
using Educational_Victoria.Models;
using Microsoft.EntityFrameworkCore;

namespace Educational_Victoria.Repositories.UserSubjectAccessRepository
{
    public class UserSubjectAccessRepository : IUserSubjectAccessRepository
    {
        private readonly EducationalDbContext _context;

        public UserSubjectAccessRepository(EducationalDbContext context)
        {
            _context = context;
        }

        public async Task<UserSubjectAccess> GetByIdAsync(int Id)
        {
            return await _context.UserSubjectsAccesses
                .Include(au => au.SubjectId)
                .Include(au => au.UserId)
                .FirstOrDefaultAsync(ua => ua.AccessId == Id);
        }

        public async Task<IEnumerable<UserSubjectAccess>> GetByUserIdAsync(int userId)
        {
            return await _context.UserSubjectsAccesses
                .Where(ua => ua.UserId == userId)
                .Include(ua => ua.Subject)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserSubjectAccess>> GetBySubjectIdAsync(int subjectId)
        {
            return await _context.UserSubjectsAccesses
                .Include(ua => ua.User)
                .Where(ua => ua.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<UserSubjectAccess> AddAsync(UserSubjectAccess userSubjectAccess)
        {
            _context.UserSubjectsAccesses.Add(userSubjectAccess);
            await _context.SaveChangesAsync();
            return userSubjectAccess;
        }

        public async Task<bool> RemoveAsync(int userId, int subjectId)
        {
            var entity = await _context.UserSubjectsAccesses.
                FirstOrDefaultAsync(ua => ua.UserId == userId && ua.SubjectId == subjectId);

            if (entity == null)
                return false;

            _context.UserSubjectsAccesses.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
