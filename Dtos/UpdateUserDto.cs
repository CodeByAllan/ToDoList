namespace ToDoList.Dtos
{
    /// <summary>
    /// Data Transfer Object for updating an existing Todo item.
    /// </summary>
    public record UpdateUserDto
    {
        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        ///  Gets or sets the last name of the user.
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string? Password { get; set; }
    }
}