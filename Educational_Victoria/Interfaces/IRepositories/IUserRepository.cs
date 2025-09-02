using Educational_Victoria.DTOs.UsersDto;
using Educational_Victoria.Interfaces.Generic;
using Educational_Victoria.Models;

namespace Educational_Victoria.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}
