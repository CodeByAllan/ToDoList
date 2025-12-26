using TodoList.Application.Dtos;
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces;

public interface ITodoItemService
{
    Task<TodoItemResponseDto> CreateAsync(CreateTodoItemDto createTodoItemDto, int userId);
    Task<IEnumerable<TodoItemResponseDto>> GetAllAsync(int userId);
    Task<TodoItemResponseDto> GetByIdAsync(int id, int userId);
    Task<TodoItemResponseDto> UpdateAsync(int id, UpdateTodoItemDto updateTodoItemDto, int userId);
    Task DeleteAsync(int id,int userId);
}