using Educational_Victoria.Data;
using Educational_Victoria.Interfaces.IRepositories;
using Educational_Victoria.Models;
using Microsoft.EntityFrameworkCore;

namespace Educational_Victoria.Repositories.UsersRepository
{
    public class UsersRepository : IUserRepository
    {
        private readonly EducationalDbContext _educationDbContext;

        public UsersRepository(EducationalDbContext educationalDbContext)
        {
            _educationDbContext = educationalDbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            try
            {
                await _educationDbContext.Users.AddAsync(user);
                await _educationDbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar usuário", ex);
            }
        }

        public async Task<User> DeleteAsync(int id)
        {
            try
            {
                var user = await _educationDbContext.Users.FindAsync(id);
                if (user != null)
                {
                    _educationDbContext.Users.Remove(user);
                    await _educationDbContext.SaveChangesAsync();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar usuário", ex);
            }
        }

        public async Task<List<User>> GetAllAsync()
        {
            try
            {
                return await _educationDbContext.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar todos os usuários", ex);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await _educationDbContext.Users
                    .Include(u => u.UserAccesses)
                    .ThenInclude(u => u.Subject)
                    .FirstOrDefaultAsync(u => u.UserId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao buscar usuário por ID", ex);
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            try
            {
                _educationDbContext.Users.Update(user);
                await _educationDbContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new("Erro ao atualizar o usuário", ex);
            }
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                return await _educationDbContext.Users
                    .Include(u => u.UserAccesses)
                    .ThenInclude(u => u.Subject)
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
            catch(Exception ex)
            {
                throw new("Erro ao localizar user por e-mail",ex);
            }
        }
    }
}
