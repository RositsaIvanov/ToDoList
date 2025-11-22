using ToDoList.Domain.ValueObjects;

namespace ToDoList.Domain.Aggregates.TodoListAggregate;

public sealed class TodoItem
{
    public TodoItemId Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Category Category { get; }
    public ProgressionHistory ProgressionHistory { get; }
    
    public bool IsCompleted => ProgressionHistory.IsCompleted;
    public decimal TotalProgress => ProgressionHistory.TotalProgress;

    private TodoItem(TodoItemId id, string title, string description, Category category)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty", nameof(description));
        
        Title = title;
        Description = description;
        Category = category ?? throw new ArgumentNullException(nameof(category));
        ProgressionHistory = new ProgressionHistory();
    }

    public static TodoItem Create(TodoItemId id, string title, string description, Category category)
        => new TodoItem(id, title, description, category);

    public void UpdateDescription(string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newDescription))
            throw new ArgumentException("Description cannot be empty", nameof(newDescription));

        if (TotalProgress > 50)
            throw new InvalidOperationException("Cannot update item with more than 50% progress");
        
        Description = newDescription;
    }

    public void RegisterProgression(Progression progression)
    {
        if (progression == null)
            throw new ArgumentNullException(nameof(progression));

        ProgressionHistory.AddProgression(progression);
    }

    public bool CanBeModified() => TotalProgress <= 50;
}
