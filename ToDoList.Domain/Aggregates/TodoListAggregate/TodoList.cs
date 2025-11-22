using ToDoList.Domain.Events;
using ToDoList.Domain.Exceptions;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Domain.Aggregates.TodoListAggregate;

public sealed class TodoList
{
    private readonly Dictionary<TodoItemId, TodoItem> _items = new();
    private int _nextId = 1;

    public IReadOnlyCollection<TodoItem> Items => _items.Values.ToList().AsReadOnly();

    // Domain events collection
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public TodoItem AddItem(string title, string description, Category category)
    {
        var id = TodoItemId.Create(_nextId++);
        var item = TodoItem.Create(id, title, description, category);
        
        _items.Add(id, item);
        
        RaiseDomainEvent(new TodoItemAddedEvent(id, title, category));
        
        return item;
    }

    public TodoItem GetItem(TodoItemId id)
    {
        if (!_items.TryGetValue(id, out var item))
            throw new TodoItemNotFoundException(id);
        
        return item;
    }

    public void UpdateItem(TodoItemId id, string newDescription)
    {
        var item = GetItem(id);
        
        if (!item.CanBeModified())
            throw new InvalidOperationException("Cannot update item with more than 50% progress");
        
        item.UpdateDescription(newDescription);
        
        RaiseDomainEvent(new TodoItemUpdatedEvent(id, newDescription));
    }

    public void RemoveItem(TodoItemId id)
    {
        var item = GetItem(id);
        
        if (!item.CanBeModified())
            throw new InvalidOperationException("Cannot remove item with more than 50% progress");
        
        _items.Remove(id);
        
        RaiseDomainEvent(new TodoItemRemovedEvent(id));
    }

    public void RegisterProgression(TodoItemId id, DateTime date, decimal percentage)
    {
        var item = GetItem(id);
        var progression = Progression.Create(date, percentage);
        
        item.RegisterProgression(progression);
        
        RaiseDomainEvent(new ProgressionRegisteredEvent(id, date, percentage));
    }

    private void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
