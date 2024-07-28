using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;

namespace TaskManagement.Application.Commands.TasksCommands.DeleteTasksCommand;

public class DeleteTasksCommand : IRequest<NotificationResult<bool>>
{
    public long TaskId { get; set; }
}