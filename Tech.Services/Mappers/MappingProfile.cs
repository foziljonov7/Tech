using AutoMapper;
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
    }
}
