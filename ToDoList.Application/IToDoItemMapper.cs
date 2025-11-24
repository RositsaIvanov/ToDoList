using ToDoList.Domain.Models;
using ToDoList.Application.Models;

namespace ToDoList.Application
{
    public interface IToDoItemMapper
    {
        ToDoItemModel MapToModel(ToDoItem todoItem);
        ToDoItem MapToDomain(ToDoItemModel toDoItemModel);
    }
}
