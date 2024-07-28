using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    UserEntity UpdateUser(UserEntity user);
    Task<IEnumerable<UserEntity>> SearchUsers(string specification);
}