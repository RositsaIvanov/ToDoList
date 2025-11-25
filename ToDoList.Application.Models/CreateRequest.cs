namespace ToDoList.Application.Models;

public class CreateRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }

    public CreateRequest()
    {
        Title = string.Empty;
        Description = string.Empty;
        Category = string.Empty;
    }
}
