using AutoMapper;
using Educational_Victoria.DTOs.SubjectDto;
using Educational_Victoria.Interfaces.Generic;
using Educational_Victoria.Interfaces.IRepositories;
using Educational_Victoria.Interfaces.IServices;
using Educational_Victoria.Models;

namespace Educational_Victoria.Services.SubjectService
{
    public class SubjectService : IService<SubjectDto, CreateSubjectDto, UpdateSubjectDto>, ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }
        public async Task<SubjectDto> CreateAsync(CreateSubjectDto createSubjectDto)
        {
            try
            {
                var subject = _mapper.Map<Subject>(createSubjectDto);
                var createdSubject = await _subjectRepository.AddAsync(subject);
                return _mapper.Map<SubjectDto>(createdSubject);
            }
            catch (Exception ex)
            {
                throw new("Erro ao criar material: ",ex);
            }
        }

        public async Task<SubjectDto> DeleteAsync(int Id)
        {
            try
            {
                var existingSubject = await _subjectRepository.GetByIdAsync(Id);
                if (existingSubject == null)
                    throw new Exception("Material não encontrado");
                var deletedUser = await _subjectRepository.DeleteAsync(existingSubject.SubjectId);

                return _mapper.Map<SubjectDto>(deletedUser);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar usuário no service", ex);
            }
        }


        public async Task<List<SubjectDto>> GetAllAsync()
        {
            try
            {
                var subject = await _subjectRepository.GetAllAsync();
                return _mapper.Map<List<SubjectDto>>(subject);
            }
            catch (Exception ex)
            {
                throw new("Erro ao pegar todos os materiais: ",ex);
            }
        }

        public async Task<SubjectDto> GetByIdAsync(int Id)
        {
            try
            {
                var subject = await _subjectRepository.GetByIdAsync(Id);
                if (subject == null || subject.SubjectId != Id) throw new("Usuário não foi encontrado.");
                return _mapper.Map<SubjectDto>(subject);
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        public async Task<SubjectDto> UpdateAsync(int id, UpdateSubjectDto updateSubjectDto)
        {
            try
            {
                var exitingSubject = await _subjectRepository.GetByIdAsync(id);
                if (exitingSubject == null || exitingSubject.SubjectId != id) 
                    throw new Exception("Erro ao localizar material.");

                _mapper.Map(updateSubjectDto, exitingSubject);
                var updateSubject = await _subjectRepository.UpdateAsync(exitingSubject);

                return _mapper.Map<SubjectDto>(updateSubject);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualziar material.",ex);
            }
        }
    }
}
