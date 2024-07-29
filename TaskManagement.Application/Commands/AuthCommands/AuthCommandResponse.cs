namespace TaskManagement.Application.Commands.AuthCommands;

public class AuthCommandResponse
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}