using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Mappers;
using ToDoList.Application.Services;
using ToDoList.Domain.Repositories;
using ToDoList.Infrastructure.Persistence.InMemory;

namespace ToDoList.Setup;

internal class DependencyInjection
{
    internal static IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ITodoListRepository, InMemoryTodoListRepository>();
        services.AddSingleton<ITodoItemMapper, TodoItemMapper>();
        services.AddSingleton<TodoListApplicationService>();
        return services;
    }
}
