using Moq;
using ToDoList.Domain.Impl;
using ToDoList.Domain.Models;
using ToDoList.Repository;

namespace ToDoList.Domain.Test;

public class TodoListServiceTests
{
    private readonly Mock<ITodoListRepository> _repositoryMock;
    private readonly ToDoListService _service;

    public TodoListServiceTests()
    {
        _repositoryMock = new Mock<ITodoListRepository>();

        // Default fake categories
        _repositoryMock
            .Setup(r => r.GetCategories())
            .Returns(new List<string> { "Work", "Personal", "Health", "Education" });

        // Default ID
        _repositoryMock
            .Setup(r => r.GetNextId())
            .Returns(1);

        _service = new ToDoListService(_repositoryMock.Object);
    }

    [Fact]
    public void WhenValidItem_ThenAddItem()
    {
        // Arrange
        var item = new ToDoItem
        {
            Id = 1,
            Title = "Task",
            Description = "Lorem Ipsum",
            Category = "Work"
        };

        // Act
        _service.AddItem(item);

        // Assert
        Assert.Single(_service.Items);
    }

    [Fact]
    public void WhenInvalidCategory_ThenAddItemThrowsException()
    {
        // Arrange
        var item = new ToDoItem
        {
            Id = 1,
            Title = "Task",
            Description = "Lorem Ipsum",
            Category = "InvalidCategory"
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _service.AddItem(item));
    }

    [Fact]
    public void WhenValidProgression_ThenRegisterProgression()
    {
        // Arrange
        var id = 1;
        var item = new ToDoItem
        {
            Id = id,
            Title = "Task",
            Description = "Lorem Ipsum",
            Category = "Work"
        };
        _service.AddItem(item);

        // Act
        _service.RegisterProgression(id, new DateTime(2025, 3, 18), 30);
        _service.RegisterProgression(id, new DateTime(2025, 3, 19), 50);
        _service.RegisterProgression(id, new DateTime(2025, 3, 20), 20);

        // Assert
        var res = _service.Items[0];
        Assert.True(res.IsCompleted);
    }

    [Fact]
    public void WhenItemDoesNotExist_RegisterProgressionThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.RegisterProgression(999, DateTime.Now, 20));
    }


    [Fact]
    public void WhenProgressionInvalidPercent_ThenThrowsException()
    {
        // Arrange
        var id = 1;
        var item = new ToDoItem
        {
            Id = id,
            Title = "Task",
            Description = "Lorem Ipsum",
            Category = "Work"
        };
        _service.AddItem(item);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.RegisterProgression(id, DateTime.Now, 0));
        Assert.Throws<ArgumentException>(() => _service.RegisterProgression(id, DateTime.Now, 101));
    }

    [Fact]
    public void WhenProgressionInvalidDate_ThenThrowsException()
    {
        // Arrange
        var id = 1;
        var item = new ToDoItem
        {
            Id = id,
            Title = "Task",
            Description = "Lorem Ipsum",
            Category = "Work"
        };
        _service.AddItem(item);
        _service.RegisterProgression(id, new DateTime(2025, 3, 18), 30);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.RegisterProgression(id, new DateTime(2025, 3, 17), 20));
    }

    [Fact]
    public void WhenProgressAbove50Percent_ThenDoNotRemoveItemAndThrowsException()
    {
        // Arrange
        var id = 1;
        var item = new ToDoItem
        {
            Id = id,
            Title = "Task",
            Description = "Lorem Ipsum",
            Category = "Work"
        };
        _service.AddItem(item);
        _service.RegisterProgression(id, new DateTime(2025, 3, 18), 60);
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _service.RemoveItem(id));
    }

    [Fact]
    public void WhenItemDoseNotExists_ThenDoNotRemoveItemAndThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.RemoveItem(9));
    }

    [Fact]
    public void WhenValidRemove_ThenItemRemoved()
    {
        var id = 1;
        var item = new ToDoItem
        {
            Id = id,
            Title = "Task",
            Description = "Lorem Ipsum",
            Category = "Work"
        };
        _service.AddItem(item);

        _service.RemoveItem(id);

        Assert.Empty(_service.Items);
    }


    [Fact]
    public void WhenProgressAbove50Percent_ThenDoNotUpdateItemAndThrowsException()
    {
        // Arrange
        var id = 1;
        var description = "Lorem Ipsum";
        var item = new ToDoItem
        {
            Id = id,
            Title = "Task",
            Description = description,
            Category = "Work"
        };
        _service.AddItem(item);
        _service.RegisterProgression(id, new DateTime(2025, 3, 18), 60);
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _service.UpdateItem(id, description));
    }

    [Fact]
    public void WhenItemDoseNotExists_ThenDoNotUpdateItemAndThrowsException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.UpdateItem(9, "Lorem Ipsum"));
    }

    [Fact]
    public void WhenValidUpdate_ThenItemUpdated()
    {
        var id = 1;
        var item = new ToDoItem
        {
            Id = id,
            Title = "Task",
            Description = "Old",
            Category = "Work"
        };
        _service.AddItem(item);

        _service.UpdateItem(id, "New Desc");

        Assert.Equal("New Desc", _service.Items[0].Description);
    }

    [Fact]
    public void PrintItems_DoesNotThrowsException()
    {
        var id = 1;
        var item = new ToDoItem
        {
            Id = id,
            Title = "Task",
            Description = "Lorem Ipsum",
            Category = "Work"
        };
        _service.AddItem(item);

        var exception = Record.Exception(() => _service.PrintItems());
        Assert.Null(exception);
    }
}