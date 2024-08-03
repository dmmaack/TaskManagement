using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.Commands.Validators;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities.Commands.Validators.UserCommands;

namespace TaskManagement.Domain.Entities.Commands.UsersCommands.CreateUsers;

public class ProducerUsersRabbitMQCommand : BaseValidations, IRequest<NotificationResult<BaseUserDTO>>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
    public string Password { get; set; }
    public int UserRule { get; set; }

    public void ValidateUser() => this.Validate(new ProducerUsersRabbitMQCommandValidator(), this);
}