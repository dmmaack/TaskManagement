using Bogus.DataSets;
using TaskManagement.Application.Commands.TasksCommands.QueriesCommand;
using TaskManagement.Core.Enums;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Tests.Fixture.CommandRequest.TasksCommand;

public class GetAllTasksCommandFixture
{
    public static GetAllTasksCommand Create_WithUserLoggedIsAdmin_CreateTasksCommand() =>
        new()
        {
            LoggedUser = new LoggedUserDTO(id: 1, name: new Name().FirstName(), userRule: 1)
        };

    public static GetAllTasksCommand Create_WithUserLoggedIsUser_CreateTasksCommand() =>
        new()
        {
            LoggedUser = new LoggedUserDTO(id: 1, name: new Name().FirstName(), userRule: 1)
        };
}