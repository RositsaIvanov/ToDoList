using Serilog;
using TodoList.Repositories;
using TodoList.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/todolist.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

Log.Information("Application Starting");

try
{
    var repository = new TodoListRepository();
    var todoList = new TodoListService(repository);

    while (true)
    {
        PrintMenu();
        string? option = Console.ReadLine();

        try
        {
            switch (option)
            {
                case "1":
                    AddItem(todoList, repository);
                    break;

                case "2":
                    UpdateItem(todoList);
                    break;

                case "3":
                    RemoveItem(todoList);
                    break;

                case "4":
                    RegisterProgress(todoList);
                    break;

                case "5":
                    todoList.PrintItems();
                    break;

                case "6":
                    Log.Information("Application Exiting");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Operation Failed (Option {Option})", option);
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

static void PrintMenu()
{
    Console.WriteLine("\nTodo List Application");
    Console.WriteLine("1. Add Item");
    Console.WriteLine("2. Update Item");
    Console.WriteLine("3. Remove Item");
    Console.WriteLine("4. Register Progression");
    Console.WriteLine("5. Print Items");
    Console.WriteLine("6. Exit");
    Console.Write("Select an option: ");
}

static int ReadInt(string label)
{
    while (true)
    {
        Console.Write(label);
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int number))
            return number;

        Console.WriteLine("Invalid number. Try again.");
    }
}

static decimal ReadDecimal(string label)
{
    while (true)
    {
        Console.Write(label);
        string? input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal result))
            return result;

        Console.WriteLine("Invalid decimal value. Try again.");
    }
}

static DateTime ReadDate(string label)
{
    while (true)
    {
        Console.Write(label);
        string? input = Console.ReadLine();

        if (DateTime.TryParse(input, out DateTime date))
            return date;

        Console.WriteLine("Invalid date format. Use yyyy-MM-dd.");
    }
}

static void AddItem(TodoListService service, TodoListRepository repo)
{
    Console.Write("Enter title: ");
    string? title = Console.ReadLine();

    Console.Write("Enter description: ");
    string? description = Console.ReadLine();

    Console.WriteLine("Available categories: " + string.Join(", ", repo.GetCategories()));
    Console.Write("Enter category: ");
    string? category = Console.ReadLine();

    int id = repo.GetNextId();
    service.AddItem(id, title, description, category);

    Log.Information("Item added: {Title}", title);
}

static void UpdateItem(TodoListService service)
{
    int id = ReadInt("Enter item ID to update: ");

    Console.Write("Enter new description: ");
    string? description = Console.ReadLine();

    service.UpdateItem(id, description);
    Log.Information("Item updated: {Id}", id);
}

static void RemoveItem(TodoListService service)
{
    int id = ReadInt("Enter item ID to remove: ");
    service.RemoveItem(id);
    Log.Information("Item removed: {Id}", id);
}

static void RegisterProgress(TodoListService service)
{
    int id = ReadInt("Enter item ID: ");
    DateTime date = ReadDate("Enter date (yyyy-MM-dd): ");
    decimal percent = ReadDecimal("Enter percent: ");

    service.RegisterProgression(id, date, percent);
    Log.Information("Progression registered: {Id} {Percent}%", id, percent);
}
