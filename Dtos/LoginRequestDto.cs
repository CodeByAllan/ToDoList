namespace ToDoList.Dtos
{
    /// <summary>
    /// DTO for user login request.
    /// </summary>
    public record LoginRequestDto
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username { get; init; } = string.Empty;
        /// <summary>
        /// The password of the user.
        /// </summary>
        public string Password { get; init; } = string.Empty;

    }
}