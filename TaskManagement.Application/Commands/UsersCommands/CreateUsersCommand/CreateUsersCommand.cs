using MediatR;
using TaskManagement.Application.Commands.Validators;
using TaskManagement.Application.Commands.Validators.UsersCommands;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Application.Commands.UsersCommands.CreateUsersCommand;

public class CreateUsersCommand : BaseValidations, IRequest<NotificationResult<BaseUserDTO>>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
    public string Password { get; set; }
    public int UserRule { get; set; }

    public void ValidateUser() => this.Validate(new CreateUsersCommandValidator(), this);

}