using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.Commands.Validators;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities.Commands.Validators.TasksCommands;

namespace TaskManagement.Domain.Entities.Commands.TasksCommands.CreateTasks;

public class CreateTasksCommand : BaseValidations, IRequest<NotificationResult<BaseTaskDTO>>
{
    public LoggedUserDTO LoggedUser { get; set; }
    
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RegisterDate { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; } 
    public long UserId { get; set; }
    public long AssignedTo { get; set; }

    public void ValidateTask() => this.Validate(new CreateTasksCommandValidator(), this);
}