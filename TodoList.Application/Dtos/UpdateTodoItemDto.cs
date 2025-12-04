namespace TodoList.Application.Dtos;

public record UpdateTodoItemDto
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public bool? IsCompleted { get; init; }
}