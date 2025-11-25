using ToDoList.Domain.Models;
namespace ToDoList.Domain.Contracts;

public interface IToDoListService
{
    void AddItem(ToDoItem item);
    void UpdateItem(int id, string description);
    void RemoveItem(int id);
    ToDoItem GetItem(int id);
    IEnumerable<ToDoItem> GetAllItems ();
    void RegisterProgression(int id, DateTime dateTime, decimal percent);
    IEnumerable<string> GetCategories();
}