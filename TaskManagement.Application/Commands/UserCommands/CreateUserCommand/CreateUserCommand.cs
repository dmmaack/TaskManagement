using MediatR;
using TaskManagement.Application.Commands.Validators;
using TaskManagement.Application.Commands.Validators.UserCommands;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Application.Commands.UserCommands.CreateUserCommand;

public class CreateUserCommand : BaseValidations, IRequest<NotificationResult<BaseUserDTO>>
{
    public CreateUserCommand(string name, string email, string userName, string password)
    {
        Name = name;
        Email = email;
        UserName = userName;
        Password = password;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public int UserRule { get; private set; }
    

    public void ValidateUser() => this.Validate(new CreateUserCommandValidator(), this);

}