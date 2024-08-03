namespace TaskManagement.Tests.Domain;

public class UserEntityTests
{
    [Fact(DisplayName = "Create_WhenUserIsValid_ReturnTrue")]
    public void Create_WhenUserIsValid_ReturnTrue()
    {
        //Dado que eu tenha um usuário.
        var userEntity = Fixture.UserEntityFixture.CreateValid_UserEntity();

        // quando todos os dados estiverem corretos e não houver erros
        userEntity.ValidateUser();

        //Entao
        Assert.True(userEntity.IsValid());

    }

    [Fact(DisplayName = "Create When User Has Invalid Password Return False")]
    public void Create_WhenUserHasInvalidPassword_ReturnFalse()
    {
        //Dado que eu tenha um usuário.
        var userEntity = Fixture.UserEntityFixture.CreateInvalidEmail_UserEntity();

        // quando algum dos dados estiver errado e houver erros
        userEntity.ValidateUser();

        //Entao
        Assert.False(userEntity.IsValid());

    }
}