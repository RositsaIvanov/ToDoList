namespace TodoList.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public List<Progression> Progressions { get; set; } = new List<Progression>();

    public bool IsCompleted => Progressions.Sum(p => p.Percent) == 100;

    public void AddProgression(DateTime date, decimal percent)
    {
        if (percent <= 0 || percent > 100)
            throw new ArgumentException("Percent must be greater than 0 and less than or equal to 100.");

        if (Progressions.Any() && date <= Progressions.Max(p => p.Date))
            throw new ArgumentException("Date must be greater than the latest progression date.");

        if (Progressions.Sum(p => p.Percent) + percent > 100)
            throw new InvalidOperationException("Total progress cannot exceed 100%.");

        Progressions.Add(new Progression { Date = date, Percent = percent });
    }
}