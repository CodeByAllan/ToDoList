namespace ToDoList.Dtos
{
    /// <summary>
    /// Data Transfer Object for creating a new Todo item.
    /// </summary>
    public record CreateTodoItemDto
    {
        /// <summary>
        /// Gets the title of the Todo item.
        /// </summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>
        /// Gets the identifier of the user creating the Todo item.
        /// </summary>
        public int UserId { get; init; }

        /// <summary>
        /// Gets the optional description of the Todo item.
        /// </summary>
        public string? Description { get; init; }
    }
}