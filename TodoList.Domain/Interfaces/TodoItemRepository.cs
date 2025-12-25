using TodoList.Domain.Entities;
namespace TodoList.Domain.Interfaces;

public interface ITodoItemRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync(int userId);
    Task<TodoItem?> GetByIdAsync(int id,int userId);
    Task AddAsync(TodoItem todoItem);
    Task UpdateAsync(TodoItem todoItem);
    Task DeleteAsync(TodoItem todoItem);
    Task<int> SaveChangesAsync();
}