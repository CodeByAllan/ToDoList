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
        /// Gets the optional description of the Todo item.
        /// </summary>
        public string? Description { get; init; }
    }
}