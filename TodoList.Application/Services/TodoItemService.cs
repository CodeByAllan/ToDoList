using TodoList.Application.Dtos;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services;

public class TodoItemService(ITodoItemRepository _todoItemRepository, IUserRepository _userRepository) : ITodoItemService
{
    public async Task<TodoItem> CreateAsync(CreateTodoItemDto createTodoItemDto)
    {
        var user = await _userRepository.GetByIdAsync(createTodoItemDto.UserId) ?? throw new KeyNotFoundException($"User with Id {createTodoItemDto.UserId} not found!");
        var todoItem = new TodoItem(title: createTodoItemDto.Title, description: createTodoItemDto.Description, userId: createTodoItemDto.UserId);
        await _todoItemRepository.AddAsync(todoItem);
        await _todoItemRepository.SaveChangesAsync();
        return todoItem;
    }
    public Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return _todoItemRepository.GetAllAsync();
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        TodoItem todoItem = await _todoItemRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"TodoItem with Id {id} not found!");
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
        await _todoItemRepository.UpdateAsync(todoItem);
        await _todoItemRepository.SaveChangesAsync();
        return todoItem;
    }
    public async Task DeleteAsync(int id)
    {
        TodoItem todoItem = await GetByIdAsync(id);
        await _todoItemRepository.DeleteAsync(todoItem);
        await _todoItemRepository.SaveChangesAsync();
    }
}