using MediatR;
using TaskManagement.Application.Commands.Validators;
using TaskManagement.Application.Commands.Validators.TaskCommands;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.TasksDTOs;

namespace TaskManagement.Application.Commands.TaskCommands.CreateTaskCommand
{
    public class CreateTaskCommand : BaseValidations, IRequest<NotificationResult<BaseTaskDTO>>
    {
        public CreateTaskCommand(string title, string description, DateTime startDate, 
            DateTime endDate, DateTime registerDate, int status, long userId, 
            long assignedTo, int priority)
        {
            this.Title = title;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.RegisterDate = registerDate;
            this.Status = status;
            this.UserId = userId;
            this.AssignedTo = assignedTo;
            this.Priority = priority;
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

        public void ValidateUser() => this.Validate(new CreateTaskCommandValidator(), this);
    }
}