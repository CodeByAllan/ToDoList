using AutoMapper;
using TodoList.Domain.Entities;
using TodoList.Application.Dtos;

namespace TodoList.Application.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserResponseDto>();
    }
}