using TodoList.Application.Dtos;
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces;

public interface ITodoItemService
{
    Task<TodoItem> CreateAsync(CreateTodoItemDto createTodoItemDto);
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem> GetByIdAsync(int id);
    Task<TodoItem> UpdateAsync(int id, UpdateTodoItemDto updateTodoItemDto);
    Task DeleteAsync(int id);
}