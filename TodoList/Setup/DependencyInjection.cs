using Microsoft.Extensions.DependencyInjection;
using ToDoList.Repository;
using ToDoList.Application;
using ToDoList.Domain.Contracts;

namespace ToDoList.Setup;

internal class DependencyInjection
{
    internal static IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ITodoListRepository, TodoListRepository>();
        services.AddSingleton<IToDoList, Application.Impl.ToDoListService>();
        services.AddSingleton<IToDoListService, Domain.Impl.ToDoListService>();
        return services;
    }
}
