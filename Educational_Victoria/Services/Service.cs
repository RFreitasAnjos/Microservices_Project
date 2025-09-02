using AutoMapper;
using Educational_Victoria.Interfaces.Generic;
using System.Linq.Expressions;

namespace Educational_Victoria.Services
{
    public class Service<TReadDto, TCreateDto, TUpdateDto> : IService<TReadDto, TCreateDto, TUpdateDto>
        where TReadDto : class
        where TCreateDto : class
        where TUpdateDto : class

    {
        private readonly IRepository<TReadDto> _repository;
        private readonly IMapper _mapper;

        public Service(IRepository<TReadDto> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TReadDto> CreateAsync(TCreateDto dto)
        {
            var entity = _mapper.Map<TReadDto>(dto);
            return await _repository.AddAsync(entity);
        }

        public async Task<TReadDto?> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<List<TReadDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TReadDto> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TReadDto?> UpdateAsync(int id, TUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            return await _repository.UpdateAsync(entity);
        }

        //public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        //{
        //    return await _repository.FindAsync(predicate);
        //}
    }

}
