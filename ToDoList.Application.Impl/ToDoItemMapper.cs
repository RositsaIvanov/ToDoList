using ToDoList.Domain.Models;
using ToDoList.Application.Models;

namespace ToDoList.Application.Impl;

public class ToDoItemMapper : IToDoItemMapper
{
    private readonly IProgressionMapper _progressionMapper;

    public ToDoItemMapper(IProgressionMapper progressionMapper)
    {
        _progressionMapper = progressionMapper;
    }

    public ToDoItemModel MapToModel(ToDoItem todoItem)
    {
        return new ToDoItemModel
        {
            Id = todoItem.Id,
            Title = todoItem.Title,
            Description = todoItem.Description,
            Category = todoItem.Category,
            Progressions = todoItem.Progressions.Select(_progressionMapper.MapToModel).ToList()
        };
    }

    public ToDoItem MapToDomain(ToDoItemModel toDoItemModel)
    {
        return new ToDoItem
        {
            Id = toDoItemModel.Id,
            Title = toDoItemModel.Title,
            Description = toDoItemModel.Description,
            Category = toDoItemModel.Category,
            Progressions = toDoItemModel.Progressions.Select(_progressionMapper.MapToDomain).ToList()
        };
    }
}
