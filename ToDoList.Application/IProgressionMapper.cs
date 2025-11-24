using ToDoList.Domain.Models;
using ToDoList.Application.Models;

namespace ToDoList.Application
{
    public interface IProgressionMapper
    {
        ProgressionModel MapToModel(Progression progression);
        Progression MapToDomain(ProgressionModel progressionModel);
    }
}
