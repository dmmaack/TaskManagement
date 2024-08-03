namespace TaskManagement.Tests.Domain;

public class TaskEntityTests
{
    [Fact(DisplayName = "Create When Task Is Valid Return True")]
    public void Create_WhenTaskIsValid_ReturnTrue()
    {
        //Dado que eu tenha um usuário.
        var taskEntity = Fixture.TaskEntityFixture.CreateValid_TaskEntity();

        // quando todos os dados estiverem corretos e não houver erros
        taskEntity.ValidateUser();

        //Entao
        Assert.True(taskEntity.IsValid());

    }

    [Fact(DisplayName = "Create When Task Has Invalid Title Return True")]
    public void Create_WhenTaskHasInvalidPassword_ReturnFalse()
    {
        //Dado que eu tenha um usuário.
        var taskEntity = Fixture.TaskEntityFixture.CreateInvalidTitle_TaskEntity();

        // quando algum dos dados estiver errado e houver erros
        taskEntity.ValidateUser();

        //Entao
        Assert.False(taskEntity.IsValid());

    }
}