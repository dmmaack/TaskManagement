using AutoMapper;
using Moq;
using TaskManagement.Application.Commands.TasksCommands.CreateTasksCommand;
using TaskManagement.Application.Commands.TasksCommands.QueriesCommand;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Tests.Configuration;
using TaskManagement.Tests.Fixture;
using TaskManagement.Tests.Fixture.CommandRequest.TasksCommand;

namespace TaskManagement.Tests.Domain.Commands.TasksCommand;

public class GetAllTasksCommandHandlerTest
{
    private readonly GetAllTasksCommandHandler _getTaksCommandHandler;
    private readonly IMapper _mapper;
    private readonly Mock<ITaskRepository> _tasksRepositoryMock;

    public GetAllTasksCommandHandlerTest()
    {
        _mapper = AutoMapperConfiguration.GetConfiguration();
        _tasksRepositoryMock = new Mock<ITaskRepository>();

        _getTaksCommandHandler = new GetAllTasksCommandHandler(_tasksRepositoryMock.Object, _mapper);
    }

    [Fact(DisplayName = "Get All Tasks When User Logged Is Admin")]
    public async Task GetAllTasks_WhenUserIsAdmin_ReturnsAList()
    {
        // Arrange
        var taskCommand = GetAllTasksCommandFixture.Create_WithUserLoggedIsAdmin_CreateTasksCommand();
        var TasksList = TaskEntityFixture.CreateListValidTask();

        var createTaskEntity = _mapper.Map<IEnumerable<TaskEntity>>(TasksList);

        _tasksRepositoryMock.Setup(x => x.GetAllAsync())
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
        var taskCommand = GetAllTasksCommandFixture.Create_WithUserLoggedIsAdmin_CreateTasksCommand();
        var TasksList = TaskEntityFixture.CreateListValidTask();

        var createTaskEntity = _mapper.Map<IEnumerable<TaskEntity>>(TasksList);

        // Act
        var result = await _getTaksCommandHandler.Handle(taskCommand, new CancellationToken());

        // Assert
        Assert.False(result.Success);
        Assert.Collection(result.Data, []);
    }

    [Fact(DisplayName = "Get All Tasks When User Logged Is Not Admin")]
    public async Task GetAllTasks_WhenUserIsNotAdmin_ReturnsAList()
    {
        // Arrange
        var taskCommand = GetAllTasksCommandFixture.Create_WithUserLoggedIsUser_CreateTasksCommand();
        var TasksList = TaskEntityFixture.CreateListValidTask();

        var createTaskEntity = _mapper.Map<IEnumerable<TaskEntity>>(TasksList);

        _tasksRepositoryMock.Setup(x => x.GetAllByAssignedToAsync(It.IsAny<long>()))
            .ReturnsAsync(() => createTaskEntity);

        // Act
        var result = await _getTaksCommandHandler.Handle(taskCommand, new CancellationToken());

        // Assert
        Assert.False(result.Data.Count() > 0);
    }
}