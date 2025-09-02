using AutoMapper;
using Educational_Victoria.DTOs.UserSubjectAccess;
using Educational_Victoria.Interfaces.Generic;
using Educational_Victoria.Interfaces.IRepositories;
using Educational_Victoria.Interfaces.IServices;
using Educational_Victoria.Models;

namespace Educational_Victoria.Services.UserSubjectAccessService
{
    public class UserSubjectAccessService : IUserSubjectAccessService
    {
        private readonly IUserSubjectAccessRepository _repository;
        private readonly IMapper _mapper;

        public UserSubjectAccessService(IUserSubjectAccessRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserSubjectAccessDto>> GetSubjectsByUserAsync(int userId)
        {
            try
            {
                var accesses = await _repository.GetByUserIdAsync(userId);
                return accesses.Select(s => _mapper.Map<UserSubjectAccessDto>(s));
            }
            catch (Exception ex)
            {
                throw new("Erro ao consultar quais usuários tem acesso a determinado material.", ex);
            }
        }

        public async Task<IEnumerable<UserSubjectAccessDto>> GetUsersBySubjectAsync(int subjectId)
        {
            try
            {
                var accesses = await _repository.GetBySubjectIdAsync(subjectId);
                return accesses.Select(u => _mapper.Map<UserSubjectAccessDto>(u));
            }
            catch (Exception ex)
            {
                throw new("Erro ao consultar quais materiais tem acesso um determinado usuário.", ex);
            }
        }

        public async Task<UserSubjectAccessDto> GrantAccessAsync(int userId, int subjectId)
        {
            try
            {
                var userSubjectAccess = new UserSubjectAccess
                {
                    UserId = userId,
                    SubjectId = subjectId,
                    PurchasedAt = DateTime.UtcNow,
                };
                var createdAccess = await _repository.AddAsync(userSubjectAccess);
                return _mapper.Map<UserSubjectAccessDto>(createdAccess);
            }
            catch (Exception ex)
            {
                throw new("Erro ao criar acesso: ", ex);
            }
        }

        public async Task<bool> RevokeAccessAsync(int userId, int subjectId)
        {
            try
            {
                return await _repository.RemoveAsync(userId, subjectId);
            }
            catch (Exception ex)
            {
                throw new("Erro ao remover permissão de acesso.", ex);
            }
        }
    }
}
