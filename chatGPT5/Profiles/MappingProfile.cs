using AutoMapper;
using chatGPT5.Models.dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}