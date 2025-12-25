namespace TodoList.Application.Dtos;

public record CreateTodoItemDto
{
    public string Title { get; init; } = String.Empty;
    public string? Description { get; init; }
}