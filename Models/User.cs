using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    /// <summary>
    /// Represents a user with immutable properties.   
    /// </summary>
    /// <remarks>
    /// This record is used to model a user in the application.
    /// All properties are immutable and can only be set during initialization.
    /// </remarks>
    [Index(nameof(Username), IsUnique = true)]
    public record User
    {
        /// <summary>
        /// Gets the unique identifier for the user.
        /// </summary>
        /// <remarks>
        /// This property is the primary key for the user entity.
        /// </remarks>
        public int Id { get; init; }
        /// <summary>
        /// Gets the first name of the user.
        /// </summary>
        /// <remarks>
        /// This property is required and defaults to an empty string.
        /// </remarks>
        public string FirstName { get; init; } = string.Empty;
        /// <summary>
        /// Gets the optional last name of the user.
        /// </summary>
        /// <remarks>
        /// This property is nullable and can be null if no last name is provided.
        /// </remarks>
        public string? LastName { get; init; }
        /// <summary>
        /// Gets the unique username of the user.
        /// </summary>
        /// <remarks>
        /// This property is required and defaults to an empty string.
        /// </remarks>
        public string Username { get; init; } = string.Empty;
        /// <summary>
        /// Gets the password of the user.
        /// </summary>
        /// <remarks>
        /// This property is required and defaults to an empty string.
        /// </remarks>
        public string Password { get; init; } = string.Empty;
        /// <summary>
        /// Gets the collection of to-do items associated with the user.
        /// </summary>
        /// <remarks>
        /// This property is initialized to an empty collection.
        /// </remarks>
        public Collection<TodoItem> TodoItems { get; init; } = [];
        /// <summary>
        /// Gets the date and time when the user was created.
        /// </summary>
        public DateTime CreatedAt { get; init; }
        /// <summary>
        /// Gets the date and time when the user was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; init; }

    }
}