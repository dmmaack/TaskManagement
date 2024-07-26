using Bogus;
using Bogus.DataSets;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests.Fixture;

public class UserEntityFixture
{
    public static UserEntity CreateValid_UserEntity(){
        return new UserEntity(id: new Randomizer().Int(0, 1000),
                              name: new Name().FullName(),
                              email: new Internet().Email(),
                              userName: new Name().FirstName(),
                              password:new Internet().Password(length: 12, prefix: "%"),
                              registerDate: new Date().Future(),
                              isActive: true
        );
    }

    public static UserEntity CreateInvalidEmail_UserEntity(){
        return new UserEntity(id: new Randomizer().Int(0, 1000),
                              name: new Name().FullName(),
                              email: new Internet().Email().Replace("@", "-"),
                              userName: new Name().FirstName(),
                              password:new Internet().Password(length: 12),
                              registerDate: new Date().Future(),
                              isActive: true
        );
    }
}