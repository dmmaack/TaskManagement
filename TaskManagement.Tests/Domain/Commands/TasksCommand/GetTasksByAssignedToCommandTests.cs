using AutoMapper;
using Moq;
using TaskManagement.Application.Commands.TasksCommands.QueriesCommand;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Tests.Configuration;
using TaskManagement.Tests.Fixture;
using TaskManagement.Tests.Fixture.CommandRequest.TasksCommand;

namespace TaskManagement.Tests.Domain.Commands.TasksCommand;

public class GetTasksByAssignedToCommandTests
{
    private readonly GetTasksByAssignedToCommandHander _getTaksCommandHandler;
    private readonly IMapper _mapper;
    private readonly Mock<ITaskRepository> _tasksRepositoryMock;

    public GetTasksByAssignedToCommandTests()
    {
        _mapper = AutoMapperConfiguration.GetConfiguration();
        _tasksRepositoryMock = new Mock<ITaskRepository>();

        _getTaksCommandHandler = new GetTasksByAssignedToCommandHander(_tasksRepositoryMock.Object, _mapper);
    }

    [Fact(DisplayName = "Get Tasks When User Logged Is Admin")]
    public async Task GetTasks_WhenUserIsAdmin_ReturnsAList()
    {
        // Arrange
        var taskCommand = GetTasksByAssignedToCommandFixture.Create_WithUserLoggedIsAdmin_CreateTasksCommand();
        var TasksList = TaskEntityFixture.CreateListValidTask();

        var createTaskEntity = _mapper.Map<IEnumerable<TaskEntity>>(TasksList);

        _tasksRepositoryMock.Setup(x => x.GetAllByAssignedToAsync(It.IsAny<long>()))
            .ReturnsAsync(() => createTaskEntity);

        // Act
        var result = await _getTaksCommandHandler.Handle(taskCommand, new CancellationToken());

        // Assert
        Assert.True(result.Data.Count() > 0);
    }

    [Fact(DisplayName = "Not Get Tasks When User Logged Is Not Admin")]
    public async Task NotGetTasks_WhenUserIsNotAdmin_ReturnsListrEmptyAndFalse()
    {
        // Arrange
        var taskCommand = GetTasksByAssignedToCommandFixture.Create_WithUserLoggedIsAdmin_CreateTasksCommand();
        var TasksList = TaskEntityFixture.CreateListValidTask();

        var createTaskEntity = _mapper.Map<IEnumerable<TaskEntity>>(TasksList);

        // Act
        var result = await _getTaksCommandHandler.Handle(taskCommand, new CancellationToken());

        // Assert
        Assert.False(result.Success);
        Assert.Collection(result.Data, []);
    }

    [Fact(DisplayName = "Not Get Tasks When User Logged Is User")]
    public async Task NotGetTasks_WhenUserIsUser_ReturnsNothing()
    {
        // Arrange
        var taskCommand = GetTasksByAssignedToCommandFixture.Create_WithUserLoggedIsUser_CreateTasksCommand();
        var TasksList = TaskEntityFixture.CreateListValidTask();

        var createTaskEntity = _mapper.Map<IEnumerable<TaskEntity>>(TasksList);

        _tasksRepositoryMock.Setup(x => x.GetAllByAssignedToAsync(It.IsAny<long>()))
            .ReturnsAsync(() => createTaskEntity);

        // Act
        var result = await _getTaksCommandHandler.Handle(taskCommand, new CancellationToken());

        // Assert
        Assert.False(result.Data.Count() > 0);
    }

    [Fact(DisplayName = "Get Tasks When User Logged Is User and Assegned To")]
    public async Task GetTasks_WhenUserIsUserAndAssegnedTo_ReturnsAList()
    {
        // Arrange
        var taskCommand = GetTasksByAssignedToCommandFixture.Create_WithUserLoggedIsUserAndAssignedTo_CreateTasksCommand();
        var TasksList = TaskEntityFixture.CreateListValidTask();

        var createTaskEntity = _mapper.Map<IEnumerable<TaskEntity>>(TasksList);

        _tasksRepositoryMock.Setup(x => x.GetAllByAssignedToAsync(It.IsAny<long>()))
            .ReturnsAsync(() => createTaskEntity);

        // Act
        var result = await _getTaksCommandHandler.Handle(taskCommand, new CancellationToken());

        // Assert
        Assert.True(result.Data.Count() > 0);
    }
}