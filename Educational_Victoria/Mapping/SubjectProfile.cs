using AutoMapper;
using Educational_Victoria.DTOs.SubjectDto;
using Educational_Victoria.Models;

namespace Educational_Victoria.Mapping
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile() 
        {
            // DTO -> Model
            CreateMap<CreateSubjectDto, Subject>();
            CreateMap<UpdateSubjectDto, Subject>();
            CreateMap<SubjectDto, Subject>();

            // Model -> DTO
            CreateMap<Subject, CreateSubjectDto>();
            CreateMap<Subject, UpdateSubjectDto>();
            CreateMap<Subject, SubjectDto>();
        }
    }
}
