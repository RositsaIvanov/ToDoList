using ToDoList.Domain.Exceptions;

namespace ToDoList.Domain.ValueObjects;

public sealed class Category : IEquatable<Category>
{
    private static readonly HashSet<string> ValidCategories = new()
    {
        "Work", "Personal", "Health", "Education"
    };

    public string Value { get; }

    private Category(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Category cannot be empty", nameof(value));
        
        if (!ValidCategories.Contains(value))
            throw new InvalidCategoryException($"Invalid category: {value}. Valid categories are: {string.Join(", ", ValidCategories)}");
            
        Value = value;
    }

    public static Category Create(string value) => new Category(value);
    
    public static IReadOnlyCollection<string> GetValidCategories() => ValidCategories.ToList().AsReadOnly();

    public bool Equals(Category? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj) => Equals(obj as Category);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Category? left, Category? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(Category? left, Category? right) => !(left == right);

    public override string ToString() => Value;
}
