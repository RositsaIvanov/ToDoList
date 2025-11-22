using ToDoList.Domain.ValueObjects;

namespace ToDoList.Tests.Domain;

public class CategoryTests
{
    [Fact]
    public void Create_ShouldReturnCategory_WhenValueIsValid()
    {
        // Act
        var category = Category.Create("Work");

        // Assert
        Assert.Equal("Work", category.Value);
    }

    [Fact]
    public void Create_ShouldThrow_WhenValueIsInvalid()
    {
        // Act & Assert
        Assert.Throws<ToDoList.Domain.Exceptions.InvalidCategoryException>(() => Category.Create("Invalid"));
    }
}
