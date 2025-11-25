using ToDoList.Domain.Contracts;
using ToDoList.Domain.Models;
using ToDoList.Repository;

namespace ToDoList.Domain.Impl;

public class ToDoListService : IToDoListService
{
    private readonly ITodoListRepository _repository;
    private readonly IList<ToDoItem> _items = new List<ToDoItem>();
    private const decimal MaxAllowedProgressBeforeLock = 50m;
    private const int BarWidth = 50;

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

    public void PrintItems()
    {
        foreach (var item in _items.OrderBy(i => i.Id))
        {
            PrintItemHeader(item);

            decimal accumulatedPercent = 0;
            foreach (var progression in item.Progressions)
            {
                accumulatedPercent += progression.Percent;
                PrintProgressBar(accumulatedPercent, progression.Date);
            }
        }
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

    private void PrintItemHeader(ToDoItem item)
    {
        Console.WriteLine($"{item.Id}) {item.Title} - {item.Description} ({item.Category}) Completed:{item.IsCompleted}");
    }

    private void PrintProgressBar(decimal accumulated, DateTime date)
    {
        int filled = (int)Math.Round(BarWidth * (accumulated / 100));
        string bar = new string('O', filled).PadRight(BarWidth, ' ');

        Console.WriteLine($"{date} - {accumulated}% |{bar}|");
    }
}