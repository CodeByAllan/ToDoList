using ToDoList.Dtos;
using ToDoList.Models;

namespace ToDoList.interfaces
{
    /// <summary>
    /// Defines the contract for User service operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an enumerable collection of User objects.
        /// </returns>
        Task<IEnumerable<User>> GetUsersAsync();
        /// <summary>
        /// Retrieves a specific user by its username asynchronously.
        /// </summary>
        /// <param name="username">The unique username of the user to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the User if found; otherwise, null.
        /// </returns>
        Task<User?> GetUserByUsernameAsync(string username);
        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="username">The unique username of the user to update.</param>
        /// <param name="updateUserDto">The data transfer object containing the User update details.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the updated User.
        /// </returns>
        Task<User> UpdateUserAsync(string username, UpdateUserDto updateUserDto);
        /// <summary>
        /// Deletes a user by its username asynchronously.
        /// </summary>
        /// <param name="username">The unique username of the user to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task DeleteUserAsync(string username);

    }
}