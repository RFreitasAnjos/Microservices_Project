using Educational_Victoria.Data;
using Educational_Victoria.Interfaces.IRepositories;
using Educational_Victoria.Models;
using Microsoft.EntityFrameworkCore;

namespace Educational_Victoria.Repositories.SubjectRepository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly EducationalDbContext _educationalDbContext;

        public SubjectRepository(EducationalDbContext educationalDbContext)
        {
            _educationalDbContext = educationalDbContext;
        }

        public async Task<Subject> AddAsync(Subject entity)
        {
            try
            {
                await _educationalDbContext.AddAsync(entity);
                await _educationalDbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new ("Erro ao criar um material",ex);
            }
        }

        public async Task<Subject> DeleteAsync(int id)
        {
            try
            {
                var subject = await _educationalDbContext.Subjects.FindAsync(id);
                if (subject != null || subject.SubjectId == id)
                {
                    _educationalDbContext.Subjects.Remove(subject);
                    await _educationalDbContext.SaveChangesAsync();
                }
                return subject;
            }
            catch (Exception ex)
            {
                throw new("Erro ao deletar material", ex);
            }
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            try
            {
                return await _educationalDbContext.Subjects.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }

        }

        public async Task<Subject> GetByIdAsync(int id)
        {
            try
            {
                return await _educationalDbContext.Subjects.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new("Erro ao buscar usuário por ID", ex);
            }
        }

        public async Task<Subject> UpdateAsync(Subject subject)
        {
            try
            {
                _educationalDbContext.Subjects.Update(subject);
                await _educationalDbContext.SaveChangesAsync();
                return subject;
            }
            catch (Exception ex) 
            {
                throw new("Erro ao atualizar o material",ex);
            }
        }
    }
}
