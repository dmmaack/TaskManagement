using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Domain.Commands.UsersCommands.Queries;

public class GetAllUsersCommand : IRequest<NotificationResult<IEnumerable<BaseUserDTO>>>
{
    
}