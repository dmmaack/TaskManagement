using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infra.Context;

namespace TaskManagement.Infra.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IUserRepository _userRepository;
    private ITaskRepository _taskRepository;
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(appDbContext: _appDbContext);

    public ITaskRepository TaskRepository => _taskRepository ??= new TaskRepository(appDbContext: _appDbContext);

    public async Task CommitAsync()
    {
        await _appDbContext.SaveChangesAsync();
    }

    public async void Dispose()
    {
        await _appDbContext.DisposeAsync();
    }
}