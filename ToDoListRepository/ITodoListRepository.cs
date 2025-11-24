namespace ToDoList.Repository;

public interface ITodoListRepository
{
    int GetNextId();
    List<string> GetCategories();
}