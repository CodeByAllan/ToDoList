namespace ToDoList.Models
{
    /// <summary>
    /// Represents a todo item with immutable properties.
    /// </summary>
    /// <remarks>
    /// This record is used to model a task or todo item in the application.
    /// All properties are immutable and can only be set during initialization.
    /// </remarks>
    public record TodoItem
    {
        /// <summary>
        /// Gets the unique identifier for the todo item.
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Gets the title or name of the todo item.
        /// </summary>
        /// <remarks>
        /// This property is required and defaults to an empty string.
        /// </remarks>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// Gets the optional description or details of the todo item.
        /// </summary>
        /// <remarks>
        /// This property is nullable and can be null if no description is provided.
        /// </remarks>
        public string? Description { get; init; }

        /// <summary>
        /// Gets a value indicating whether the todo item has been completed.
        /// </summary>
        /// <remarks>
        /// Defaults to false when the item is created.
        /// </remarks>
        public bool IsCompleted { get; init; } = false;

        /// <summary>
        /// Gets the date and time when the todo item was created.
        /// </summary>
        public DateTime CreatedAt { get; init; }

        /// <summary>
        /// Gets the date and time when the todo item was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; init; }
    }
}