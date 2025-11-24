namespace ToDoList.Repository;

public class TodoListRepository : ITodoListRepository
{
    private int _currentId = 0;
    private readonly List<string> _categories = new List<string> { "Work", "Personal", "Health", "Education" };

    public int GetNextId()
    {
        return ++_currentId;
    }

    public List<string> GetCategories()
    {
        return _categories;
    }
}