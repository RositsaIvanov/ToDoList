namespace ToDoList.Application.Models;

public class UpdateRequest
{
    public string Description { get; set; }

    public UpdateRequest(string description)
    {
        Description = description;
    }
}
