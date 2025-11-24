using Microsoft.Extensions.DependencyInjection;
using TodoList.Interfaces;
using TodoList.Repositories;
using TodoList.Services;

namespace TodoList.Setup;

internal class DependencyInjection
{
    internal static IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ITodoListRepository, TodoListRepository>();
        services.AddSingleton<ITodoList, TodoListService>();
        return services;
    }
}
