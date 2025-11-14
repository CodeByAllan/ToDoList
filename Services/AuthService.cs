using ToDoList.Dtos;
using ToDoList.interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    /// <summary>
    /// Service for authentication operations.
    /// </summary>
    public class AuthService(IUserRepository _userRepository, ITokenService _tokenService) : IAuthService
    {
        /// <summary>
        /// Logs in a user and returns a JWT token if successful.
        /// </summary>
        /// <param name="loginRequestDto">The login request containing username and password.</param>
        /// <returns>A JWT token if login is successful; otherwise, null.</returns>
        /// <exception cref="ArgumentNullException">Thrown when loginRequest is null.</exception>
        /// <remarks>
        /// This method checks if the user exists and if the provided password matches. If both conditions are met, it generates and returns a JWT token.
        /// </remarks>
        public async Task<string?> Login(LoginRequestDto loginRequestDto)
        {
            var userIsExist = await _userRepository.GetUserByUsernameAsync(loginRequestDto.Username);
            if (userIsExist == null || userIsExist.Password != loginRequestDto.Password)
            {
                return null;
            }
            return _tokenService.CreateToken(userIsExist);
        }
        /// <summary>
        /// Registers a new user and returns a JWT token upon successful registration.
        /// </summary>
        /// <param name="registerRequestDto">The registration request containing user details.</param>
        /// <returns>A JWT token if registration is successful.</returns>
        /// <exception cref="ArgumentException">Thrown when required fields are missing or username is already taken.</exception>
        /// <remarks>
        /// This method validates the registration details, checks for existing usernames, creates a new user, and generates a JWT token for the newly registered user.
        /// </remarks>
        public async Task<string?> Register(RegisterRequestDto registerRequestDto)
        {
            if (string.IsNullOrWhiteSpace(registerRequestDto.FirstName))
            {
                throw new ArgumentException("FirstName is required!");
            }
            if (string.IsNullOrWhiteSpace(registerRequestDto.Username))
            {
                throw new ArgumentException("Username is required!");
            }
            if (string.IsNullOrWhiteSpace(registerRequestDto.Password))
            {
                throw new ArgumentException("Password is required!");
            }
            var userIsExist = await _userRepository.GetUserByUsernameAsync(registerRequestDto.Username);
            if (userIsExist is not null)
            {
                throw new ArgumentException($"Username {registerRequestDto.Username} is already taken!");
            }
            var dateTimeNow = DateTime.UtcNow;
            var user = new User
            {
                FirstName = registerRequestDto.FirstName,
                LastName = registerRequestDto.LastName,
                Username = registerRequestDto.Username,
                Password = registerRequestDto.Password,
                CreatedAt = dateTimeNow,
                UpdatedAt = dateTimeNow
            };
            await _userRepository.CreateUserAsync(user);
            await _userRepository.SaveUserAsync();
            return _tokenService.CreateToken(user);
        }
    }
}