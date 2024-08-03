using System.Linq.Expressions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infra.Context;

namespace TaskManagement.Infra.Repositories;

public class UserRepository(AppDbContext appDbContext) : BaseRepository<UserEntity>(appDbContext), IUserRepository
{
    public async Task<IEnumerable<UserEntity>> SearchUsers(string specification)
    {
        Expression<Func<UserEntity, bool>> predicate = 
            predicate => predicate.UserName.Contains(specification) ||
                         predicate.Name.Contains(specification) ||
                         predicate.Email.Contains(specification);        

        return await this.GetAsync(predicate);
    }

    public UserEntity UpdateUser(UserEntity user)
    {
        return this._appDbContext.Users.Update(user).Entity;
    }

    public async Task<UserEntity> GetUserLogin(string email, string password)
    {
        Expression<Func<UserEntity, bool>> predicate = 
            predicate => predicate.Email.Equals(email) && predicate.Password.Equals(password);

        return (await this.GetAsync(predicate)).FirstOrDefault();
    }
}