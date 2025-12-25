using TodoList.Application.Dtos;
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces;

public interface ITodoItemService
{
    Task<TodoItem> CreateAsync(CreateTodoItemDto createTodoItemDto, int userId);
    Task<IEnumerable<TodoItem>> GetAllAsync(int userId);
    Task<TodoItem> GetByIdAsync(int id, int userId);
    Task<TodoItem> UpdateAsync(int id, UpdateTodoItemDto updateTodoItemDto, int userId);
    Task DeleteAsync(int id,int userId);
}