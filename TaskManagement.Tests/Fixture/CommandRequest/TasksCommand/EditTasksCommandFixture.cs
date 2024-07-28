using Bogus;
using Bogus.DataSets;
using TaskManagement.Application.Commands.TasksCommands.UpdateTasksCommand;
using TaskManagement.Core.Enums;

namespace TaskManagement.Tests.Fixture.CommandRequest.TasksCommand;

public class EditTasksCommandFixture
{
    public static UpdateTasksCommand CreateValid_EditTasksCommand() =>
        new()
        {
            Id = new Faker().Random.Int(),
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

    public static UpdateTasksCommand CreateWithInvalidId_EditTasksCommand() =>
        new()
        {
            Id = 0,
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