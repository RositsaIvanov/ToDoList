using ToDoList.Domain.ValueObjects;

namespace ToDoList.Domain.Events;

public sealed class TodoItemUpdatedEvent : IDomainEvent
{
    public TodoItemId ItemId { get; }
    public string NewDescription { get; }
    public DateTime OccurredOn { get; }

    public TodoItemUpdatedEvent(TodoItemId itemId, string newDescription)
    {
        ItemId = itemId;
        NewDescription = newDescription;
        OccurredOn = DateTime.UtcNow;
    }
}
