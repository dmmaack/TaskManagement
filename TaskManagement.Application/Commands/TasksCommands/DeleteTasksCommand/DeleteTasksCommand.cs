using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Application.Commands.TasksCommands.DeleteTasksCommand;

public class DeleteTasksCommand : IRequest<NotificationResult<bool>>
{
    public LoggedUserDTO LoggedUser { get; set; }
    
    public long TaskId { get; set; }
}