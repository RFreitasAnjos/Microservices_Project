using AutoMapper;
using Educational_Victoria.DTOs.UsersDto;
using Educational_Victoria.Interfaces.Generic;
using Educational_Victoria.Interfaces.IRepositories;
using Educational_Victoria.Interfaces.IServices;
using Educational_Victoria.Models;
using BCrypt.Net;   

namespace Educational_Victoria.Services.UsersService
{
    public class UserService : IService<UserDto, CreateUserDto, UpdateUserDto>, IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                    throw new Exception("Usuário não encontrado");

                return _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                throw new("Erro ao pegar usuário por ID: ",ex);
            }
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return _mapper.Map<List<UserDto>>(users);
            }
            catch(Exception ex)
            {
                throw new("Erro ao pegar todos usuários: ", ex);
            }
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            try
            {
                if (dto.Role.ToString() != "Adm" && dto.Role.ToString() != "User")
                    throw new ArgumentException("Role inválida. Use 'Adm' ou 'User'.");
                var entity = _mapper.Map<User>(dto);
                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                entity.Password = hashedPassword;
                
                var createdUser = await _userRepository.AddAsync(entity);
                return _mapper.Map<UserDto>(createdUser);
            }
            catch (Exception ex)
            {
                throw new("Erro ao criar usuário: ",ex);
            }
        }

        public async Task<UserDto> UpdateAsync(int id, UpdateUserDto dto)
        {
            try
            {
                var existingUser = await _userRepository.GetByIdAsync(id);
                if (existingUser == null || existingUser.UserId != id)
                    throw new Exception("Usuário não encontrado");

                _mapper.Map(dto, existingUser);
                var updatedUser = await _userRepository.UpdateAsync(existingUser);

                return _mapper.Map<UserDto>(updatedUser);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        public async Task<UserDto> DeleteAsync(int id)
        {
            try
            {
                var existingUser = await _userRepository.GetByIdAsync(id);
                Console.WriteLine(existingUser);
                if (existingUser == null)
                    throw new Exception("Usuário não encontrado");

                var deletedUser = await _userRepository.DeleteAsync(existingUser.UserId);

                return _mapper.Map<UserDto>(deletedUser);
            }
            catch (Exception ex)
            {
                throw new("Erro ao deletar usuário: ",ex);
            }
        }
    }
}
