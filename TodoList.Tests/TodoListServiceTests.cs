using System;
using System.Collections.Generic;
using TodoList.Repositories;
using TodoList.Services;
using Xunit;
using Moq;

namespace TodoList.Tests
{
    public class TodoListServiceTests
    {
        private readonly Mock<ITodoListRepository> _repositoryMock;
        private readonly TodoListService _service;

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

            _service = new TodoListService(_repositoryMock.Object);
        }

        [Fact]
        public void WhenValidItem_ThenAddItem()
        {
            // Arrange
            var title = "Task";
            var description = "Lorem Ipsum";
            var category = "Work";

            // Act
            _service.AddItem(1, title, description, category);

            // Assert
            Assert.Single(_service.Items);
        }

        [Fact]
        public void WhenInvalidCategory_ThenAddItemThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                _service.AddItem(1, "Task", "Lorem Ipsum", "InvalidCategory"));
        }

        [Fact]
        public void WhenValidProgression_ThenRegisterProgression()
        {
            // Arrange
            var id = 1;
            _service.AddItem(id, "Task", "Lorem Ipsum", "Work");

            // Act
            _service.RegisterProgression(id, new DateTime(2025, 3, 18), 30);
            _service.RegisterProgression(id, new DateTime(2025, 3, 19), 50);
            _service.RegisterProgression(id, new DateTime(2025, 3, 20), 20);

            // Assert
            var item = _service.Items[0];
            Assert.True(item.IsCompleted);
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
            _service.AddItem(id, "Task", "Lorem Ipsum", "Work");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.RegisterProgression(id, DateTime.Now, 0));
            Assert.Throws<ArgumentException>(() => _service.RegisterProgression(id, DateTime.Now, 101));
        }

        [Fact]
        public void WhenProgressionInvalidDate_ThenThrowsException()
        {
            // Arrange
            var id = 1;
            _service.AddItem(id, "Task", "Lorem Ipsum", "Work");
            _service.RegisterProgression(id, new DateTime(2025, 3, 18), 30);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.RegisterProgression(id, new DateTime(2025, 3, 17), 20));
        }

        [Fact]
        public void WhenProgressAbove50Percent_ThenDoNotRemoveItemAndThrowsException()
        {
            // Arrange
            var id = 1;
            _service.AddItem(id, "Task", "Lorem Ipsum", "Work");
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
            _service.AddItem(id, "Task", "Lorem Ipsum", "Work");

            _service.RemoveItem(id);

            Assert.Empty(_service.Items);
        }


        [Fact]
        public void WhenProgressAbove50Percent_ThenDoNotUpdateItemAndThrowsException()
        {
            // Arrange
            var id = 1;
            var description = "Lorem Ipsum";
            _service.AddItem(id, "Task", description, "Work");
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
            _service.AddItem(id, "Task", "Old", "Work");

            _service.UpdateItem(id, "New Desc");

            Assert.Equal("New Desc", _service.Items[0].Description);
        }

        [Fact]
        public void PrintItems_DoesNotThrowsException()
        {
            var id = 1;
            _service.AddItem(id, "Task", "Lorem Ipsum", "Work");

            var exception = Record.Exception(() => _service.PrintItems());
            Assert.Null(exception);
        }
    }
}