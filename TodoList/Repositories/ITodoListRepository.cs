namespace TodoList.Repositories
{
    public interface ITodoListRepository
    {
        int GetNextId();
        List<string> GetCategories();
    }
}