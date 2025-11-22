namespace ToDoList.Domain.ValueObjects;

public sealed class Progression : IEquatable<Progression>
{
    public DateTime Date { get; }
    public decimal Percentage { get; }

    private Progression(DateTime date, decimal percentage)
    {
        if (percentage <= 0 || percentage > 100)
            throw new ArgumentException("Percentage must be between 0 and 100", nameof(percentage));
        
        Date = date;
        Percentage = percentage;
    }

    public static Progression Create(DateTime date, decimal percentage) 
        => new Progression(date, percentage);

    public bool Equals(Progression? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Date == other.Date && Percentage == other.Percentage;
    }

    public override bool Equals(object? obj) => Equals(obj as Progression);

    public override int GetHashCode() => HashCode.Combine(Date, Percentage);

    public override string ToString() => $"{Date:yyyy-MM-dd}: {Percentage}%";
}
