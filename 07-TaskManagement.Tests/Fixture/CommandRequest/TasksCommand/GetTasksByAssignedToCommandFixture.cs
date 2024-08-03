using Bogus;
using Bogus.DataSets;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities.Commands.TasksCommands.Queries;

namespace TaskManagement.Tests.Fixture.CommandRequest.TasksCommand;

public class GetTasksByAssignedToCommandFixture
{
    public static GetTasksByAssignedToCommand Create_WithUserLoggedIsAdmin_CreateTasksCommand() =>
        new()
        {
            AssegnedTo = new Randomizer().Int(0, 10),
            LoggedUser = new LoggedUserDTO(id: 1, name: new Name().FirstName(), userRule: 1)
        };

    public static GetTasksByAssignedToCommand Create_WithUserLoggedIsUser_CreateTasksCommand() =>
        new()
        {
            AssegnedTo = new Randomizer().Int(0, 10),
            LoggedUser = new LoggedUserDTO(id: 1, name: new Name().FirstName(), userRule: 2)
        };

    public static GetTasksByAssignedToCommand Create_WithUserLoggedIsUserAndAssignedTo_CreateTasksCommand() =>
        new()
        {
            AssegnedTo = 10,
            LoggedUser = new LoggedUserDTO(id: 10, name: new Name().FirstName(), userRule: 2)
        };
}