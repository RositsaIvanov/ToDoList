using ToDoList.Application.Models;
using ToDoList.Domain.Contracts;
using ToDoList.Domain.Models;

namespace ToDoList.Application.Impl;

public class ToDoListService : IToDoList
{
    private readonly IToDoListService _service;
    private readonly IToDoItemMapper _mapper;

    public ToDoListService(IToDoListService service, IToDoItemMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public ToDoItemModel? GetItem(int id)
    {
        var item = _service.GetItem(id);
        return item == null ? null : _mapper.MapToModel(item);
    }

    public IEnumerable<ToDoItemModel> GetAllItems() 
    {
        return _service.GetAllItems().Select(item => _mapper.MapToModel(item));     
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
