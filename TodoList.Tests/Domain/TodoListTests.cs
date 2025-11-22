using ToDoList.Domain.Aggregates.TodoListAggregate;
using ToDoList.Domain.ValueObjects;

namespace ToDoList.Tests.Domain;

public class TodoListTests
{
    [Fact]
    public void AddItem_ShouldAddItem_WhenValid()
    {
        // Arrange
        var todoList = new TodoList();
        var category = Category.Create("Work");

        // Act
        var item = todoList.AddItem("Title", "Description", category);

        // Assert
        Assert.Single(todoList.Items);
        Assert.Equal("Title", item.Title);
        Assert.Equal("Description", item.Description);
        Assert.Equal(category, item.Category);
        Assert.Contains(todoList.DomainEvents, e => e is ToDoList.Domain.Events.TodoItemAddedEvent);
    }

    [Fact]
    public void UpdateItem_ShouldUpdateDescription_WhenValid()
    {
        // Arrange
        var todoList = new TodoList();
        var item = todoList.AddItem("Title", "Description", Category.Create("Work"));

        // Act
        todoList.UpdateItem(item.Id, "New Description");

        // Assert
        Assert.Equal("New Description", item.Description);
        Assert.Contains(todoList.DomainEvents, e => e is ToDoList.Domain.Events.TodoItemUpdatedEvent);
    }

    [Fact]
    public void RegisterProgression_ShouldAddProgression_WhenValid()
    {
        // Arrange
        var todoList = new TodoList();
        var item = todoList.AddItem("Title", "Description", Category.Create("Work"));
        var date = DateTime.UtcNow;

        // Act
        todoList.RegisterProgression(item.Id, date, 50m);

        // Assert
        Assert.Single(item.ProgressionHistory.Progressions);
        Assert.Equal(50m, item.TotalProgress);
        Assert.Contains(todoList.DomainEvents, e => e is ToDoList.Domain.Events.ProgressionRegisteredEvent);
    }

    [Fact]
    public void RegisterProgression_ShouldThrow_WhenTotalProgressExceeds100()
    {
        // Arrange
        var todoList = new TodoList();
        var item = todoList.AddItem("Title", "Description", Category.Create("Work"));
        todoList.RegisterProgression(item.Id, DateTime.UtcNow, 50m);

        // Act & Assert
        Assert.Throws<ToDoList.Domain.Exceptions.InvalidProgressionException>(() => 
            todoList.RegisterProgression(item.Id, DateTime.UtcNow.AddDays(1), 60m));
    }
}
