namespace ToDoList.Application.Models;

public class ToDoItemModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public List<ProgressionModel> Progressions { get; set; } = new List<ProgressionModel>();

    public bool IsCompleted => Progressions.Sum(p => p.Percent) == 100;
}