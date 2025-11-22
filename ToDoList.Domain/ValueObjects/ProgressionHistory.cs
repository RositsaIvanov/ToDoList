using ToDoList.Domain.Exceptions;

namespace ToDoList.Domain.ValueObjects;

public sealed class ProgressionHistory
{
    private readonly List<Progression> _progressions = new();
    
    public IReadOnlyList<Progression> Progressions => _progressions.AsReadOnly();
    public decimal TotalProgress => _progressions.Sum(p => p.Percentage);
    public bool IsCompleted => TotalProgress == 100m;

    public void AddProgression(Progression progression)
    {
        if (progression == null)
            throw new ArgumentNullException(nameof(progression));

        if (_progressions.Any() && progression.Date <= _progressions.Max(p => p.Date))
            throw new InvalidProgressionException("Progression date must be after the latest progression date");

        if (TotalProgress + progression.Percentage > 100)
            throw new InvalidProgressionException($"Total progress cannot exceed 100%. Current: {TotalProgress}%, Attempting to add: {progression.Percentage}%");

        _progressions.Add(progression);
    }

    public ProgressionHistory Clone()
    {
        var clone = new ProgressionHistory();
        foreach (var progression in _progressions)
        {
            clone._progressions.Add(progression);
        }
        return clone;
    }
}
