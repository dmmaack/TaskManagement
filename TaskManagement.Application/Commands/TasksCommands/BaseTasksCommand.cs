using MediatR;
using TaskManagement.Application.Commands.Validators;
using TaskManagement.Domain.DTO.TasksDTOs;

namespace TaskManagement.Application.Commands.TasksCommands;

public class BaseTasksCommand : BaseValidations, IRequest<BaseTaskDTO>
{
    public BaseTasksCommand(string title, string description, DateTime startDate, DateTime endDate, DateTime registerDate, int status, int priority, long userId, long assignedTo)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        RegisterDate = registerDate;
        Status = status;
        Priority = priority;
        UserId = userId;
        AssignedTo = assignedTo;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RegisterDate { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; } 
    public long UserId { get; set; }
    public long AssignedTo { get; set; }
}