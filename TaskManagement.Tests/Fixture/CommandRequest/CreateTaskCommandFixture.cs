using Bogus.DataSets;
using TaskManagement.Application.Commands.TaskCommands.CreateTaskCommand;
using TaskManagement.Core.Enums;

namespace TaskManagement.Tests.Fixture.CommandRequest;

public class CreateTaskCommandFixture
{
    public static CreateTaskCommand CreateValid_CreateTaskCommand() => 
        new CreateTaskCommand(title: string.Join(" ", new Lorem().Words(num: 4)),
                description: new Lorem().Text(), 
                startDate: DateTime.Now.AddDays(1), 
                endDate: DateTime.Now.AddDays(2), 
                registerDate: DateTime.Now, 
                status: (int)StatusEnum.Pending, 
                userId: 2, 
                assignedTo: 3,
                priority: (int)PriorityTaskEnum.Middle
            );
                
        public static CreateTaskCommand CreateInvalidTitle_CreateTaskCommand() => 
            new CreateTaskCommand(title: string.Empty,
                description: new Lorem().Text(), 
                startDate: DateTime.Now.AddDays(1), 
                endDate: DateTime.Now.AddDays(2), 
                registerDate: DateTime.Now, 
                status: (int)StatusEnum.Pending, 
                userId: 2, 
                assignedTo: 3,
                priority: (int)PriorityTaskEnum.Middle
            );
}