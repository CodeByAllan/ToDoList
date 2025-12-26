namespace TodoList.Application.Dtos;

public record UserResponseDto
{
    public int ID { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Username { get; init; } = null!;
    public TodoItemResponseDto[] TodoItems { get; init; } = [];
}