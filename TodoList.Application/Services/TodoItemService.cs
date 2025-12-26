using AutoMapper;
using TodoList.Application.Dtos;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;

namespace TodoList.Application.Services;

public class TodoItemService(ITodoItemRepository _todoItemRepository, IUserRepository _userRepository, IMapper _mapper) : ITodoItemService
{
    public async Task<TodoItemResponseDto> CreateAsync(CreateTodoItemDto createTodoItemDto, int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId) ?? throw new KeyNotFoundException($"User with Id {userId} not found!");
        var todoItem = new TodoItem(title: createTodoItemDto.Title, description: createTodoItemDto.Description, userId: userId);
        await _todoItemRepository.AddAsync(todoItem);
        await _todoItemRepository.SaveChangesAsync();
        return _mapper.Map<TodoItemResponseDto>(todoItem);
    }
    public async Task<IEnumerable<TodoItemResponseDto>> GetAllAsync(int userId)
    {
        IEnumerable<TodoItem> todoItems = await _todoItemRepository.GetAllAsync(userId);
        return _mapper.Map<IEnumerable<TodoItemResponseDto>>(todoItems);
    }

    public async Task<TodoItemResponseDto> GetByIdAsync(int id, int userId)
    {
        TodoItem todoItem = await _todoItemRepository.GetByIdAsync(id, userId) ?? throw new KeyNotFoundException($"TodoItem with Id {id} not found!");
        return _mapper.Map<TodoItemResponseDto>(todoItem);
    }

    public async Task<TodoItemResponseDto> UpdateAsync(int id, UpdateTodoItemDto updateTodoItemDto, int userId)
    {
        TodoItem todoItem = await _todoItemRepository.GetByIdAsync(id, userId) ?? throw new KeyNotFoundException($"TodoItem with Id {id} not found!");
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
        return _mapper.Map<TodoItemResponseDto>(todoItem);
    }
    public async Task DeleteAsync(int id, int userId)
    {
        TodoItem todoItem = await _todoItemRepository.GetByIdAsync(id, userId) ?? throw new KeyNotFoundException($"TodoItem with Id {id} not found!");
        await _todoItemRepository.DeleteAsync(todoItem);
        await _todoItemRepository.SaveChangesAsync();
    }
}