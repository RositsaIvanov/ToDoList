using ToDoList.Application.Models;

namespace ToDoList.Application;

public interface IToDoList
{
    void AddItem(int id, string title, string description, string category);
    ToDoItemModel? GetItem(int id);
    IEnumerable<ToDoItemModel> GetAllItems();
    void UpdateItem(int id, string description);
    void RemoveItem(int id);
    void RegisterProgression(int id, DateTime dateTime, decimal percent);
    void PrintItems();
    IEnumerable<string> GetCategories();
}