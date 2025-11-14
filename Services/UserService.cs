using ToDoList.Dtos;
using ToDoList.interfaces;
using ToDoList.Models;

namespace ToDoList.Services

{
    /// <summary>
    /// Service for managing users.
    /// </summary>
    public class UserService(IUserRepository _repository) : IUserService
    {
        /// <summary>
        /// Deletes a user by username.
        /// </summary>
        /// <param name="username">The username of the user to delete.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the user with the specified username is not found.</exception>
        public async Task DeleteUserAsync(string username)
        {
            var userIsExist = await _repository.GetUserByUsernameAsync(username);
            if (userIsExist is null)
            {
                throw new KeyNotFoundException($"User with Username {username} not found!");
            }
            await _repository.DeleteUserAsync(userIsExist);
            await _repository.SaveUserAsync();
        }
        /// <summary>
        /// Retrieves a user by username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user if found; otherwise, null.</returns>
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var user =  await _repository.GetUserByUsernameAsync(username);
            if (user == null)
                {
                    throw new KeyNotFoundException($"User with Username {username} not found!");
                }
            return  user;
        }
        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A collection of all users.</returns>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _repository.GetUsersAsync();
        }
        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="username">The username of the user to update.</param>
        /// <param name="updateUserDto">The user data transfer object containing updated user details.</param>
        /// <returns>The updated user.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the user with the specified username is not found.</exception>
        /// <exception cref="ArgumentException">Thrown when the new username is already taken.</exception>
        public async Task<User> UpdateUserAsync(string username, UpdateUserDto updateUserDto)
        {
            var userIsExist = await _repository.GetUserByUsernameAsync(username);

            if (userIsExist is null)
            {
                throw new KeyNotFoundException($"User with Username {username} not found!");
            }

            if (!string.IsNullOrWhiteSpace(updateUserDto.Username) && updateUserDto.Username != username)
            {
                var userWithNewUsername = await _repository.GetUserByUsernameAsync(updateUserDto.Username);
                if (userWithNewUsername is not null)
                {
                    throw new ArgumentException($"Username {updateUserDto.Username} is already taken!");
                }

            }
            var updatedUser = userIsExist with
            {
                FirstName = updateUserDto.FirstName ?? userIsExist.FirstName,
                LastName = updateUserDto.LastName ?? userIsExist.LastName,
                Username = updateUserDto.Username ?? userIsExist.Username,
                Password = updateUserDto.Password ?? userIsExist.Password,
                UpdatedAt = DateTime.UtcNow
            };
            await _repository.UpdateUserAsync(userIsExist, updatedUser);
            await _repository.SaveUserAsync();
            return updatedUser;
        }
    }
}