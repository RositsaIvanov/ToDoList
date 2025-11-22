namespace ToDoList.Domain.Exceptions;

public class InvalidProgressionException : Exception
{
    public InvalidProgressionException(string message) : base(message)
    {
    }
}
