using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories;

public interface ITaskRepository : IBaseRepository<TaskEntity>
{
    void UpdateTask(TaskEntity task);
    Task<IEnumerable<TaskEntity>> GetAllByUserIdAsync(long userId);
}