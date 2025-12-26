using TodoList.Application.Dtos;
using TodoList.Domain.Entities;

namespace TodoList.Application.Interfaces;

public interface IUserService
{
    Task<UserResponseDto> GetByIdAsync(int id);
    Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto updateUserDto);
    Task DeleteAsync(int id);
}