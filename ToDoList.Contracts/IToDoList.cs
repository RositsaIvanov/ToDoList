using ToDoList.Domain.Models;

namespace ToDoList.Domain.Contracts;

public interface IToDoList
{
    void AddItem(ToDoItem item);
    void UpdateItem(int id, string description);
    void RemoveItem(int id);
    void RegisterProgression(int id, DateTime dateTime, decimal percent);
    void PrintItems();
}