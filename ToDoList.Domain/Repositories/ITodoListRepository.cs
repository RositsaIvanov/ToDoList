using ToDoList.Domain.Aggregates.TodoListAggregate;

namespace ToDoList.Domain.Repositories;

public interface ITodoListRepository
{
    TodoList Get();
    void Save(TodoList todoList);
}
