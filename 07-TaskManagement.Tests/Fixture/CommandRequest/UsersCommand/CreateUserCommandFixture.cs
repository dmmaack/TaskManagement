using Bogus.DataSets;
using TaskManagement.Core.Enums;
using TaskManagement.Domain.Entities.Commands.UsersCommands.CreateUsers;

namespace TaskManagement.Tests.Fixture.CommandRequest.UsersCommand;

public class CreateUsersCommandFixture
{
    public static CreateUsersCommand CreateValid_CreateUsersCommand() =>
        new()
        {
            Name = new Name().FullName(),
            Email = new Internet().Email(),
            UserName = new Name().FirstName(),
            RegisterDate = DateTime.Now,
            IsActive = true,
            Password = new Internet().Password(length: 12, prefix: "%"),
            UserRule = (int)UserRulesEnum.User
        };

    public static CreateUsersCommand CreateInvalid_CreateUsersCommand() =>
        new()
        {
            Name = new Name().FullName(),
            Email = new Internet().Email(),
            UserName = new Name().FirstName(),
            RegisterDate = DateTime.Now,
            IsActive = true,
            Password = new Internet().Password(length: 5, prefix: "%"),
            UserRule = (int)UserRulesEnum.User
        };
}