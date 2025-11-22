using ToDoList.Domain.ValueObjects;

namespace ToDoList.Domain.Events;

public sealed class TodoItemAddedEvent : IDomainEvent
{
    public TodoItemId ItemId { get; }
    public string Title { get; }
    public Category Category { get; }
    public DateTime OccurredOn { get; }

    public TodoItemAddedEvent(TodoItemId itemId, string title, Category category)
    {
        ItemId = itemId;
        Title = title;
        Category = category;
        OccurredOn = DateTime.UtcNow;
    }
}
