using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Mappers;
using ToDoList.Application.Services;
using ToDoList.Domain.Repositories;
using ToDoList.Infrastructure.Persistence.InMemory;

namespace ToDoList.WebApi.Registrations;

internal static class ServicesRegistration
{
    internal static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<ITodoListRepository, InMemoryTodoListRepository>();
        services.AddSingleton<ITodoItemMapper, TodoItemMapper>();
        services.AddSingleton<TodoListApplicationService>();
        return services;
    }
}
