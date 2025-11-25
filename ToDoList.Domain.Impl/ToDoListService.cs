using ToDoList.Domain.Contracts;
using ToDoList.Domain.Models;
using ToDoList.Repository;

namespace ToDoList.Domain.Impl;

public class ToDoListService : IToDoListService
{
    private readonly ITodoListRepository _repository;
    private readonly IList<ToDoItem> _items = new List<ToDoItem>();
    private const decimal MaxAllowedProgressBeforeLock = 50m;

    public ToDoListService(ITodoListRepository repository)
    {
        _repository = repository;
    }

    public void AddItem(ToDoItem item)
    {
        ValidateCategory(item.Category);
        _items.Add(item);
    }

    public ToDoItem GetItem(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
            throw new ArgumentException("Item not found.");
        return item;
    }

    public IEnumerable<ToDoItem> GetAllItems()
    {
        return _items.ToList();
    }

    public void UpdateItem(int id, string description)
    {
        var item = GetAndValidateItem(id,
            "Cannot update an item with more than 50% progress.");

        item!.Description = description;
    }

    public void RemoveItem(int id)
    {
        var item = GetAndValidateItem(id,
            "Cannot remove an item with more than 50% progress.");

        _items.Remove(item!);
    }

    public void RegisterProgression(int id, DateTime dateTime, decimal percent)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
            throw new ArgumentException("Item not found.");

        item.AddProgression(dateTime, percent);
    }

    public IEnumerable<string> GetCategories()
    {
        return _repository.GetCategories();
    }

    private void ValidateCategory(string category)
    {
        if (!_repository.GetCategories().Contains(category))
            throw new ArgumentException("Invalid category.");
    }

    private ToDoItem GetAndValidateItem(int id, string message)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null)
            throw new ArgumentException("Item not found.");

        if (item.Progressions.Sum(p => p.Percent) > MaxAllowedProgressBeforeLock)
            throw new InvalidOperationException(message);

        return item;
    }
}