using ToDoList.Domain.Aggregates.TodoListAggregate;
using ToDoList.Application.DTOs;

namespace ToDoList.Application.Mappers;

public interface ITodoItemMapper
{
    TodoItemDto MapToDto(TodoItem item);
}

public class TodoItemMapper : ITodoItemMapper
{
    public TodoItemDto MapToDto(TodoItem item)
    {
        return new TodoItemDto
        {
            Id = item.Id.Value,
            Title = item.Title,
            Description = item.Description,
            Category = item.Category.Value,
            Progressions = item.ProgressionHistory.Progressions
                .Select(p => new ProgressionDto 
                { 
                    Date = p.Date, 
                    Percentage = p.Percentage 
                })
                .ToList(),
            IsCompleted = item.IsCompleted,
            TotalProgress = item.TotalProgress
        };
    }
}
