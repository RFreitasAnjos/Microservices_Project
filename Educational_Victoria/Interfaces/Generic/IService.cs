using System.Linq.Expressions;

namespace Educational_Victoria.Interfaces.Generic
{
    public interface IService<TReadDto, TCreateDto, TUpdateDto> 
        where TReadDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        Task<TReadDto> GetByIdAsync(int id);
        Task<List<TReadDto>> GetAllAsync();
        Task<TReadDto> CreateAsync(TCreateDto dto);
        Task<TReadDto> UpdateAsync(int id, TUpdateDto dto);
        Task<TReadDto> DeleteAsync(int id);
    }
}
