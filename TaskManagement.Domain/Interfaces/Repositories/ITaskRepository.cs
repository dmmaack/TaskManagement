using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories;

public interface ITaskRepository : IBaseRepository<TaskEntity>
{
    TaskEntity UpdateTask(TaskEntity task);
    Task<IEnumerable<TaskEntity>> GetAllByUserIdAsync(long userId);
    Task<IEnumerable<TaskEntity>> GetAllByAssignedToAsync(long userId);
}