namespace TodoList.Application.Dtos;
public record TodoItemResponseDto
{
    public int ID { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public bool IsCompleted { get; init; }
}