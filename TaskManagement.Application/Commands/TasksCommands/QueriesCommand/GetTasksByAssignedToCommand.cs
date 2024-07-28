using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.TasksDTOs;

namespace TaskManagement.Application.Commands.TasksCommands.QueriesCommand;

public class GetTasksByAssignedToCommand : IRequest<NotificationResult<IEnumerable<BaseTaskDTO>>>
{
    public long AssegnedTo { get; set; }
}