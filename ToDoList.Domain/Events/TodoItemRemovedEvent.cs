using ToDoList.Domain.ValueObjects;

namespace ToDoList.Domain.Events;

public sealed class TodoItemRemovedEvent : IDomainEvent
{
    public TodoItemId ItemId { get; }
    public DateTime OccurredOn { get; }

    public TodoItemRemovedEvent(TodoItemId itemId)
    {
        ItemId = itemId;
        OccurredOn = DateTime.UtcNow;
    }
}
