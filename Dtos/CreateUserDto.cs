namespace ToDoList.Dtos
{
    /// <summary>
    /// Data Transfer Object for creating a new Todo item.
    /// </summary>
    public record CreateUserDto
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;
        /// <summary>
        ///  Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}