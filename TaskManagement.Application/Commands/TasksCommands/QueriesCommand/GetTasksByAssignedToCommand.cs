using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Application.Commands.TasksCommands.QueriesCommand;

public class GetTasksByAssignedToCommand : IRequest<NotificationResult<IEnumerable<BaseTaskDTO>>>
{
    public LoggedUserDTO LoggedUser { get; set; }
    
    public long AssegnedTo { get; set; }
}