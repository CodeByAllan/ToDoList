using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using TodoList.Infrastructure.Persistence;

namespace TodoList.Infrastructure.Repositories;

public class TodoItemRepository(ApplicationDbContext _applicationDbContext) : ITodoItemRepository
{
    public async Task AddAsync(TodoItem todoItem)
    {
        await _applicationDbContext.TodoItems.AddAsync(todoItem);
    }

    public Task DeleteAsync(TodoItem todoItem)
    {
        _applicationDbContext.TodoItems.Remove(todoItem);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync(int userId)
    {
        return await _applicationDbContext.TodoItems.Where(item => item.UserId == userId).ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id, int userId)
    {
        return await _applicationDbContext.TodoItems.Where(item => item.UserId == userId && item.ID == id).FirstOrDefaultAsync();
    }
    public Task UpdateAsync(TodoItem todoItem)
    {
        _applicationDbContext.TodoItems.Update(todoItem);
        return Task.CompletedTask;
    }
    public async Task<int> SaveChangesAsync()
    {
        return await _applicationDbContext.SaveChangesAsync();
    }
}