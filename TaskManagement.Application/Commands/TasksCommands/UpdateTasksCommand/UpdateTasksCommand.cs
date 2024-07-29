using MediatR;
using TaskManagement.Application.Commands.Validators;
using TaskManagement.Application.Commands.Validators.TaskCommands;
using TaskManagement.Application.Commands.Validators.TasksCommands;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Application.Commands.TasksCommands.UpdateTasksCommand;

public class UpdateTasksCommand : BaseValidations, IRequest<NotificationResult<BaseTaskDTO>>
{
    public LoggedUserDTO LoggedUser { get; set; }
    
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RegisterDate { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; } 
    public long UserId { get; set; }
    public long AssignedTo { get; set; }

    public bool ValidateTask() => this.Validate(new EditTaskCommandValidator(), this);
}