using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infra.Context;

namespace TaskManagement.Infra.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    public readonly AppDbContext _appDbContext;

    public BaseRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    private IQueryable<T> BuildQuery(Expression<Func<T, bool>> expression)
            => _appDbContext.Set<T>().Where(expression);

    public async Task<T> CreateAsync(T obj)
    {
        await _appDbContext.AddAsync(obj);
        return obj;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var obj = _appDbContext.Set<T>().AsNoTracking();
        return await obj.ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression, bool asNoTracking = true)
        => asNoTracking
            ? await BuildQuery(expression)
                   .AsNoTracking()
                   .ToListAsync()
            : await BuildQuery(expression)
                   .ToListAsync();

    public async Task<T> GetByIdAsync(long id, bool asNoTracking = true)
    {
        Expression<Func<T, bool>> expression = expression => expression.Id.Equals(id);

        return asNoTracking
            ? await BuildQuery(expression)
                   .AsNoTracking()
                   .FirstOrDefaultAsync()
            : await BuildQuery(expression)
                   .FirstOrDefaultAsync();
    }

    public async Task RemoveAsync(long id)
    {
        var obj = await GetByIdAsync(id);

        if (obj != null)
            _appDbContext.Remove(obj);
    }
}