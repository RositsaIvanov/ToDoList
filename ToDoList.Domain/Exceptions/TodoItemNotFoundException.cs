using ToDoList.Domain.ValueObjects;

namespace ToDoList.Domain.Exceptions;

public class TodoItemNotFoundException : Exception
{
    public TodoItemNotFoundException(TodoItemId id) 
        : base($"TodoItem with ID {id.Value} was not found")
    {
    }
}
