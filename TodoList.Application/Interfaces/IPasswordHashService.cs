namespace TodoList.Application.Interfaces;
public interface IPasswordHashService
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}