using TodoList.Application.Dtos;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services;

public class TodoItemService(ITodoItemRepository _repository) : ITodoItemService
{
    public async Task<TodoItem> CreateAsync(CreateTodoItemDto createTodoItemDto)
    {
        var todoItem = new TodoItem(title: createTodoItemDto.Title, description: createTodoItemDto.Description);
        await _repository.AddAsync(todoItem);
        await _repository.SaveChangesAsync();
        return todoItem;
    }
    public Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        TodoItem todoItem = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"TodoItem with Id {id} not found!");
        return todoItem;
    }

    public async Task<TodoItem> UpdateAsync(int id, UpdateTodoItemDto updateTodoItemDto)
    {
        TodoItem todoItem = await GetByIdAsync(id);
        if (updateTodoItemDto.Title != null)
        {
            todoItem.UpdateTitle(updateTodoItemDto.Title);
        }
        if (updateTodoItemDto.Description != null)
        {
            todoItem.UpdateDescription(updateTodoItemDto.Description);
        }
        if (updateTodoItemDto.IsCompleted.HasValue)
        {
            if (updateTodoItemDto.IsCompleted.Value)
            {
                todoItem.MarkAsCompleted();
            }
            else
            {
                todoItem.MarkAsIncomplete();
            }
        }
        await _repository.UpdateAsync(todoItem);
        await _repository.SaveChangesAsync();
        return todoItem;
    }
    public async Task DeleteAsync(int id)
    {
        TodoItem todoItem = await GetByIdAsync(id);
        await _repository.DeleteAsync(todoItem);
        await _repository.SaveChangesAsync();
    }
}