using AutoMapper;
using Moq;
using TaskManagement.Application.Commands.TasksCommands.CreateTasksCommand;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Tests.Configuration;
using TaskManagement.Tests.Fixture.CommandRequest.TasksCommand;

namespace TaskManagement.Tests.Domain.Commands.TasksCommand;

public class CreateTaskCommandHandlerTests
{
    private readonly CreateTasksCommandHandler _createTaksCommandHandler;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CreateTaskCommandHandlerTests()
    {
        _mapper = AutoMapperConfiguration.GetConfiguration();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _createTaksCommandHandler = new CreateTasksCommandHandler(_unitOfWorkMock.Object, _mapper);
    }

    [Fact(DisplayName = "Create When Is Valid Return Success True")]
    public async Task Create_WhenIsValid_ReturnSuccessTrue()
    {
        // arrante
        var createTaskCommand = CreateTasksCommandFixture.CreateValid_CreateTasksCommand();
        var createTaskEntity = _mapper.Map<TaskEntity>(createTaskCommand);

        _unitOfWorkMock.Setup(
            set => set.TaskRepository.CreateAsync(It.IsAny<TaskEntity>())
        ).ReturnsAsync(createTaskEntity);


        // act
        var result = await _createTaksCommandHandler.Handle(createTaskCommand, new CancellationToken());

        // assert
        Assert.True(result.Success);
    }

    [Fact(DisplayName = "Create When Is Invalid Title Return Success False")]
    public async Task Create_WhenIsInvalidTitle_ReturnSuccessFalse()
    {
        // arrante
        var createTaskCommand = CreateTasksCommandFixture.CreateInvalidTitle_CreateTasksCommand();

        // act
        var result = await _createTaksCommandHandler.Handle(createTaskCommand, new CancellationToken());

        // assert
        Assert.False(result.Success);
    }

    [Fact(DisplayName = "Create When Is Invalid Start Date Return Success False")]
    public async Task Create_WhenIsInvalidStartDate_ReturnSuccessFalse()
    {
        // arrante
        var createTaskCommand = CreateTasksCommandFixture.CreateInvalidStartDate_CreateTasksCommand();

        // act
        var result = await _createTaksCommandHandler.Handle(createTaskCommand, new CancellationToken());

        // assert
        Assert.False(result.Success);
    }



}

internal class CreateTaskCommand
{
}