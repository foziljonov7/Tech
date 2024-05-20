using AutoMapper;
using Tech.DAL.DTOs.CourseDTOs;
using Tech.DAL.DTOs.SubjectDTOs;
using Tech.DAL.DTOs.UserDTOs;
using Tech.Domain.Entities;

namespace Tech.Services.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserForCreateDto>().ReverseMap();
        CreateMap<User, UserForUpdateDto>().ReverseMap();
        CreateMap<User, UserForChangePasswordDto>().ReverseMap();
        CreateMap<User, UserForChangeRoleDto>().ReverseMap();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Course, CourseForCreateDto>().ReverseMap();
        CreateMap<Course, CourseForUpdateDto>().ReverseMap();
        CreateMap<Subject, SubjectDto>().ReverseMap();
        CreateMap<Subject, SubjectForCreateDto>().ReverseMap(); 
        CreateMap<Subject, SubjectForUpdateDto>().ReverseMap();
    }
}
