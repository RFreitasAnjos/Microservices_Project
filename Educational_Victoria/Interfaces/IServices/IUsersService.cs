using Educational_Victoria.DTOs.UsersDto;
using Educational_Victoria.Interfaces.Generic;
using Educational_Victoria.Models;

namespace Educational_Victoria.Interfaces.IServices
{
    public interface IUsersService : IService<UserDto, CreateUserDto, UpdateUserDto>
    {
        // Implementar contratos para lógicas específicas.
    }
}
