using Bogus.DataSets;
using TaskManagement.Application.Commands.TasksCommands.CreateTasksCommand;
using TaskManagement.Core.Enums;

namespace TaskManagement.Tests.Fixture.CommandRequest.TasksCommand;

public class CreateTasksCommandFixture
{
    public static CreateTasksCommand CreateValid_CreateTasksCommand() =>
        new()
        {
            Title = string.Join(" ", new Lorem().Words(num: 4)),
            Description = new Lorem().Text(),
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(2),
            RegisterDate = DateTime.UtcNow,
            Status = (int)StatusEnum.Pending,
            UserId = 2,
            AssignedTo = 3,
            Priority = (int)PriorityTaskEnum.Middle
        };

    public static CreateTasksCommand CreateInvalidTitle_CreateTasksCommand() =>
        new()
        {
            Title = string.Empty,
            Description = new Lorem().Text(),
            StartDate = DateTime.UtcNow.AddDays(1),
            EndDate = DateTime.UtcNow.AddDays(2),
            RegisterDate = DateTime.UtcNow,
            Status = (int)StatusEnum.Pending,
            UserId = 2,
            AssignedTo = 3,
            Priority = (int)PriorityTaskEnum.Middle
        };

    public static CreateTasksCommand CreateInvalidStartDate_CreateTasksCommand() =>
        new()
            {
                Title = string.Join(" ", new Lorem().Words(num: 4)),
                Description = new Lorem().Text(),
                StartDate = DateTime.UtcNow.AddDays(-1),
                EndDate = DateTime.UtcNow.AddDays(2),
                RegisterDate = DateTime.UtcNow,
                Status = (int)StatusEnum.Pending,
                UserId = 2,
                AssignedTo = 3,
                Priority = (int)PriorityTaskEnum.Middle
        };
}