using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;

namespace TaskManagement.Application.Commands.AuthCommands;

public class AuthCommand : IRequest<NotificationResult<AuthCommandResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}