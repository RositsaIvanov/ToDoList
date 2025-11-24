using ToDoList.Domain.Contracts;
using ToDoList.Domain.Models;

namespace ToDoList.Application.Impl;

public class ToDoListService : IToDoList
{
    private readonly IToDoListService _service;

    public ToDoListService(IToDoListService service)
    {
        _service = service;
    }

    public void AddItem(int id, string title, string description, string category)
    {
        _service.AddItem(new ToDoItem
        {
            Id = id,
            Title = title,
            Description = description,
            Category = category
        });
    }

    public void UpdateItem(int id, string description)
    {
        _service.UpdateItem(id, description);
    }

    public void RemoveItem(int id)
    {
        _service.RemoveItem(id);
    }

    public void RegisterProgression(int id, DateTime dateTime, decimal percent)
    {
        _service.RegisterProgression(id, dateTime, percent);
    }

    public void PrintItems()
    {
        _service.PrintItems();
    }
}
