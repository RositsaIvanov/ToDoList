using ToDoList.Domain.ValueObjects;

namespace ToDoList.Domain.Events;

public sealed class ProgressionRegisteredEvent : IDomainEvent
{
    public TodoItemId ItemId { get; }
    public DateTime ProgressionDate { get; }
    public decimal Percentage { get; }
    public DateTime OccurredOn { get; }

    public ProgressionRegisteredEvent(TodoItemId itemId, DateTime progressionDate, decimal percentage)
    {
        ItemId = itemId;
        ProgressionDate = progressionDate;
        Percentage = percentage;
        OccurredOn = DateTime.UtcNow;
    }
}
