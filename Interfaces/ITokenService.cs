using ToDoList.Models;

namespace ToDoList.interfaces
{
    /// <summary>
    /// Interface for token service to create authentication tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates an authentication token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to create the token.</param>
        /// <returns>A string representing the authentication token.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the user is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when token creation fails.</exception>
        /// <remarks> Implementations should ensure the token is securely generated and signed.</remarks>
        string CreateToken(User user);
    }
}