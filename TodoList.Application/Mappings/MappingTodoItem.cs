using AutoMapper;
using TodoList.Domain.Entities;
using TodoList.Application.Dtos;

namespace TodoList.Application.Mappings;
public class MappingTodoItem : Profile
{
    public MappingTodoItem()
    {
        CreateMap<TodoItem, TodoItemResponseDto>();
    }
}