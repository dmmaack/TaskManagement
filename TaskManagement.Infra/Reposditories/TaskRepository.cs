using System.Linq.Expressions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infra.Context;

namespace TaskManagement.Infra.Reposditories;

public class TaskRepository(AppDbContext appDbContext) : BaseRepository<TaskEntity>(appDbContext), ITaskRepository
{
    public async Task<IEnumerable<TaskEntity>> GetAllByUserIdAsync(long userId)
    {
        Expression<Func<TaskEntity, bool>> predicate =
            predicate => predicate.UserId.Equals(userId);

        return await this.GetAsync(predicate);

    }

    public void UpdateTask(TaskEntity task)
    {
        this._appDbContext.Update(task);
    }
}