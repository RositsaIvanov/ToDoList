namespace ToDoList.Domain.ValueObjects;

public sealed class TodoItemId : IEquatable<TodoItemId>
{
    public int Value { get; }

    private TodoItemId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("TodoItem ID must be positive", nameof(value));
        Value = value;
    }

    public static TodoItemId Create(int value) => new TodoItemId(value);

    public bool Equals(TodoItemId? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => Equals(obj as TodoItemId);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(TodoItemId? left, TodoItemId? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(TodoItemId? left, TodoItemId? right) => !(left == right);

    public override string ToString() => Value.ToString();
}
