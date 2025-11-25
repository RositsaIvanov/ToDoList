using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application;
using ToDoList.Application.Impl;
using ToDoList.Domain.Contracts;
using ToDoList.Repository;

namespace ToDoList.WebApi.Registrations;

internal static class ServicesRegistration
{
    internal static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<ITodoListRepository, TodoListRepository>();
        services.AddSingleton<IToDoList, ToDoListService>();
        services.AddSingleton<IToDoListService, Domain.Impl.ToDoListService>();
        services.AddSingleton<IProgressionMapper, ProgressionMapper>();
        services.AddSingleton<IToDoItemMapper, ToDoItemMapper>();
        return services;
    }
}
