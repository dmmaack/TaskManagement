using System.Linq.Expressions;

namespace TaskManagement.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(long id, bool asNoTracking = true);
    Task<T> CreateAsync(T obj);
}