using AutoMapper;
using Educational_Victoria.DTOs.SubjectDto;
using Educational_Victoria.DTOs.UsersDto;
using Educational_Victoria.DTOs.UserSubjectAccess;
using Educational_Victoria.Models;

namespace Educational_Victoria.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            // DTO -> Model (User)
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<UserDto, User>();
            // Model -> DTO  (User)
            CreateMap<User, CreateUserDto>();
            CreateMap<User, UpdateUserDto>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserAccesses, opt => opt.MapFrom(src => src.UserAccesses));
        }
    }
}
