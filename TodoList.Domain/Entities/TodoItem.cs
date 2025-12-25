namespace TodoList.Domain.Entities;

public class TodoItem
{
    public int ID { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; }
    public int UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    private TodoItem() { }
    public TodoItem(string title, int userId, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        Title = title;
        Description = description;
        IsCompleted = false;
        if (!int.IsPositive(userId))
            throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be a positive integer.");
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    public void MarkAsCompleted()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            UpdatedAt = DateTime.UtcNow;
        }
    }
    public void MarkAsIncomplete()
    {
        if (IsCompleted)
        {
            IsCompleted = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));
        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }
    public void UpdateDescription(string? description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}