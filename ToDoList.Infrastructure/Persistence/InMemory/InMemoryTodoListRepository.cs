using ToDoList.Domain.Aggregates.TodoListAggregate;
using ToDoList.Domain.Repositories;

namespace ToDoList.Infrastructure.Persistence.InMemory;

public class InMemoryTodoListRepository : ITodoListRepository
{
    private TodoList? _todoList;

    public TodoList Get()
    {
        return _todoList ??= new TodoList();
    }

    public void Save(TodoList todoList)
    {
        _todoList = todoList ?? throw new ArgumentNullException(nameof(todoList));
    }
}
