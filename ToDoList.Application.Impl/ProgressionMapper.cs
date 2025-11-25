using ToDoList.Domain.Models;
using ToDoList.Application.Models;

namespace ToDoList.Application.Impl
{
    public class ProgressionMapper : IProgressionMapper
    {
        public ProgressionModel MapToModel(Progression progression)
        {
            return new ProgressionModel
            {
                Date = progression.Date,
                Percent = progression.Percent
            };
        }

        public Progression MapToDomain(ProgressionModel progressionModel)
        {
            return new Progression
            {
                Date = progressionModel.Date,
                Percent = progressionModel.Percent
            };
        }
    }
}
