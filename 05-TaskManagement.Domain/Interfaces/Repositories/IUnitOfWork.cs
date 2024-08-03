namespace TaskManagement.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ITaskRepository TaskRepository { get; }
    
    Task CommitAsync();
    void Dispose();
}