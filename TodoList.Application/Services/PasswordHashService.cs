using Microsoft.AspNetCore.Identity;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;

namespace TodoList.Application.Services;

public class PasswordHashService(IPasswordHasher<User> _passwordHasher) : IPasswordHashService
{
    public string HashPassword(string password)
    {
       return _passwordHasher.HashPassword(null!, password);
    }
    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
        return result == PasswordVerificationResult.Success ;
    }
}
