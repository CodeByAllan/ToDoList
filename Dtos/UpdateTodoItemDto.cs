namespace ToDoList.Dtos
{
    /// <summary>
    /// Data transfer object for updating an existing todo item.
    /// </summary>
    public record UpdateTodoItemDto
    {
        /// <summary>
        /// The new title for the todo item.
        /// </summary>>
        public string? Title { get; init; }
        /// <summary>
        /// The new description for the todo item.
        /// </summary>
        public string? Description { get; init; }
        /// <summary>
        /// The new completion state of the todo item.
        /// </summary>
        public bool? IsCompleted { get; init; }
    }
}