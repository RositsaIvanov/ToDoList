namespace ToDoList.Domain.Exceptions;

public class InvalidCategoryException : Exception
{
    public InvalidCategoryException(string message) : base(message)
    {
    }
}
