using AutoMapper;
using Educational_Victoria.DTOs.UserSubjectAccess;
using Educational_Victoria.Models;

namespace Educational_Victoria.Mapping
{
    public class UserSubjectAccessProfile : Profile
    {
        public UserSubjectAccessProfile() 
        {
            // DTO -> Model (UserSubjectAccess)
            CreateMap<UserSubjectAccessDto, UserSubjectAccess>();
            // Model -> DTO (UserSubjectAccess)
            CreateMap<UserSubjectAccess, UserSubjectAccessDto>()
                .ForMember(dest => dest.SubjectTitle, opt => opt.MapFrom(src => src.Subject.Title));
        }
    }
}
