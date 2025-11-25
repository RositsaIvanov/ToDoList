namespace ToDoList.Application.Models;

public class RegisterProgressionRequest
{
    public int Percentage { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;

    public RegisterProgressionRequest(DateTime date, int percentage)
    {
        Date = date;
        Percentage = percentage;
    }
}
