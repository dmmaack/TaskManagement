using System.Linq.Expressions;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infra.Context;

namespace TaskManagement.Infra.Reposditories;

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

    public void UpdateUser(UserEntity user)
    {
        this._appDbContext.Users.Update(user);
    }
}