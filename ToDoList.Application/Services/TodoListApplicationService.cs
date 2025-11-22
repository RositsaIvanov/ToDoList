using ToDoList.Application.DTOs;
using ToDoList.Application.Mappers;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Application.Services;

public class TodoListApplicationService
{
    private readonly ITodoListRepository _repository;
    private readonly ITodoItemMapper _mapper;

    public TodoListApplicationService(ITodoListRepository repository, ITodoItemMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public TodoItemDto AddItem(string title, string description, string categoryName)
    {
        var todoList = _repository.Get();
        var category = Category.Create(categoryName);
        
        var item = todoList.AddItem(title, description, category);
        
        _repository.Save(todoList);
        
        return _mapper.MapToDto(item);
    }

    public TodoItemDto GetItem(int id)
    {
        var todoList = _repository.Get();
        var itemId = TodoItemId.Create(id);
        var item = todoList.GetItem(itemId);
        
        return _mapper.MapToDto(item);
    }

    public IEnumerable<TodoItemDto> GetAllItems()
    {
        var todoList = _repository.Get();
        return todoList.Items.Select(_mapper.MapToDto);
    }

    public TodoItemDto UpdateItem(int id, string description)
    {
        var todoList = _repository.Get();
        var itemId = TodoItemId.Create(id);
        
        todoList.UpdateItem(itemId, description);
        
        _repository.Save(todoList);
        
        var updatedItem = todoList.GetItem(itemId);
        return _mapper.MapToDto(updatedItem);
    }

    public void RemoveItem(int id)
    {
        var todoList = _repository.Get();
        var itemId = TodoItemId.Create(id);
        
        todoList.RemoveItem(itemId);
        
        _repository.Save(todoList);
    }

    public TodoItemDto RegisterProgression(int id, DateTime date, decimal percentage)
    {
        var todoList = _repository.Get();
        var itemId = TodoItemId.Create(id);
        
        todoList.RegisterProgression(itemId, date, percentage);
        
        _repository.Save(todoList);
        
        var updatedItem = todoList.GetItem(itemId);
        return _mapper.MapToDto(updatedItem);
    }

    public IEnumerable<string> GetCategories()
    {
        return Category.GetValidCategories();
    }
}
