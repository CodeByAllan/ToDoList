using ToDoList.Dtos;

namespace ToDoList.interfaces
{
    /// <summary>
    /// Interface for authentication services.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Logins a user with the provided login request.
        /// </summary>
        /// <param name="loginRequest">The login request DTO.</param>
        /// <returns>A JWT token if login is successful; otherwise, null.</returns>
        Task<string?> Login(LoginRequestDto loginRequest);
        /// <summary>
        /// Registers a new user with the provided registration request.
        /// </summary>
        /// <param name="registerRequestDto">The registration request DTO.</param>
        /// <returns>A JWT token if registration is successful; otherwise, null.</returns>
        Task<string?> Register(RegisterRequestDto registerRequestDto);
    }
}