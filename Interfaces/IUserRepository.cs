using ToDoList.Models;

namespace ToDoList.interfaces
{
    /// <summary>
    /// Defines the contract for data access operations on User entities.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of all User objects.
        /// </returns>
        Task<IEnumerable<User>> GetUsersAsync();
        /// <summary>
        /// Retrieves a specific user by its identifier asynchronously.
        /// </summary>
        /// <param name="username">The unique username of the user to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the User if found; otherwise, null.
        /// </returns>
        Task<User?> GetUserByUsernameAsync(string username);
        /// <summary>
        /// Creates a new user asynchronously.
        /// </summary>
        /// <param name="user">The User object to be created.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task CreateUserAsync(User user);
        /// <summary>
        /// Saves changes to the data store asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task SaveUserAsync();
        /// <summary>
        /// Updates an existing user with new values asynchronously.
        /// </summary>
        /// <param name="user">The original User object to be updated.</param>
        /// <param name="updateUser">The User object containing the updated values.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task UpdateUserAsync(User user, User updateUser);
        /// <summary>
        /// Deletes a user asynchronously.
        /// </summary>
        /// <param name="user">The User object to be deleted.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        Task DeleteUserAsync(User user);
    }
}