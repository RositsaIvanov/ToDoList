namespace ToDoList.Application.DTOs;

public class TodoItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<ProgressionDto> Progressions { get; set; } = new();
    public bool IsCompleted { get; set; }
    public decimal TotalProgress { get; set; }
}
