using System.Linq.Expressions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infra.Context;

namespace TaskManagement.Infra.Repositories;

public class TaskRepository(AppDbContext appDbContext) : BaseRepository<TaskEntity>(appDbContext), ITaskRepository
{
    public async Task<IEnumerable<TaskEntity>> GetAllByUserIdAsync(long userId)
    {
        Expression<Func<TaskEntity, bool>> predicate =
            predicate => predicate.UserId.Equals(userId);

        return await this.GetAsync(predicate);

    }

    public async Task<IEnumerable<TaskEntity>> GetAllByAssignedToAsync(long userId)
    {
        Expression<Func<TaskEntity, bool>> predicate =
            predicate => predicate.AssignedTo.Equals(userId);

        return await this.GetAsync(predicate);

    }

    public TaskEntity UpdateTask(TaskEntity task)
    {
        return this._appDbContext.Update(task).Entity;
    }
}