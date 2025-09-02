using Educational_Victoria.DTOs.SubjectDto;
using Educational_Victoria.Interfaces.Generic;
using Educational_Victoria.Models;

namespace Educational_Victoria.Interfaces.IServices
{
    public interface ISubjectService : IService<SubjectDto, CreateSubjectDto, UpdateSubjectDto>
    {
        // Implementar contratos para lógicas específicas.
    }
}
