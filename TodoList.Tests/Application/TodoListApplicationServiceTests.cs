using Moq;
using ToDoList.Application.DTOs;
using ToDoList.Application.Mappers;
using ToDoList.Application.Services;
using ToDoList.Domain.Aggregates.TodoListAggregate;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Tests.Application;

public class TodoListApplicationServiceTests
{
    private readonly Mock<ITodoListRepository> _mockRepository;
    private readonly Mock<ITodoItemMapper> _mockMapper;
    private readonly TodoListApplicationService _service;

    public TodoListApplicationServiceTests()
    {
        _mockRepository = new Mock<ITodoListRepository>();
        _mockMapper = new Mock<ITodoItemMapper>();
        _service = new TodoListApplicationService(_mockRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public void AddItem_ShouldAddItemToRepository_WhenValid()
    {
        // Arrange
        var todoList = new TodoList();
        _mockRepository.Setup(r => r.Get()).Returns(todoList);
        _mockMapper.Setup(m => m.MapToDto(It.IsAny<TodoItem>()))
            .Returns((TodoItem item) => new TodoItemDto { Title = item.Title });

        // Act
        var result = _service.AddItem("Title", "Description", "Work");

        // Assert
        _mockRepository.Verify(r => r.Save(todoList), Times.Once);
        Assert.Equal("Title", result.Title);
    }

    [Fact]
    public void UpdateItem_ShouldUpdateItemInRepository_WhenValid()
    {
        // Arrange
        var todoList = new TodoList();
        var item = todoList.AddItem("Title", "Description", Category.Create("Work"));
        _mockRepository.Setup(r => r.Get()).Returns(todoList);
        _mockMapper.Setup(m => m.MapToDto(It.IsAny<TodoItem>()))
            .Returns((TodoItem i) => new TodoItemDto { Description = i.Description });

        // Act
        var result = _service.UpdateItem(item.Id.Value, "New Description");

        // Assert
        _mockRepository.Verify(r => r.Save(todoList), Times.Once);
        Assert.Equal("New Description", result.Description);
    }
}
