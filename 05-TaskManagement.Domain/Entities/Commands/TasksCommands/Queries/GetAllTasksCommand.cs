using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Domain.Entities.Commands.TasksCommands.Queries;

public class GetAllTasksCommand : IRequest<NotificationResult<IEnumerable<BaseTaskDTO>>>
{
    public LoggedUserDTO LoggedUser { get; set; }
}