using System.Linq.Expressions;
using AutoMapper;
using Moq;
using TaskManagement.Application.Commands.UsersCommands.CreateUsers;
using TaskManagement.Application.Services.UsersServices;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;
using TaskManagement.Tests.Configuration;
using TaskManagement.Tests.Fixture.CommandRequest.UsersCommand;

namespace TaskManagement.Tests.Domain.Commands;

public class CreateUsersCommandHandlerTests
{
    private readonly CreateUsersCommandHandler _CreateUsersCommandHandler;
    private readonly IMapper _mapper;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ICreateUserServices _createUserServices;

    public CreateUsersCommandHandlerTests()
    {
        _mapper = AutoMapperConfiguration.GetConfiguration();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _createUserServices = new CreateUserServices(_unitOfWorkMock.Object, _mapper);
        _CreateUsersCommandHandler = new CreateUsersCommandHandler(_unitOfWorkMock.Object, _mapper, _createUserServices);
    }

    [Fact(DisplayName = "Create Valid User Return Success True")]
    public async Task Create_WhenIsValid_ReturnSuccessTrue()
    {
        // arrange
        var userToCreateCommand = CreateUsersCommandFixture.CreateValid_CreateUsersCommand();
        var userEntity = _mapper.Map<UserEntity>(userToCreateCommand);
        
        _unitOfWorkMock.Setup(
            repo => repo.UserRepository.GetAsync(It.IsAny<Expression<Func<UserEntity, bool>>>(), true)
            ).ReturnsAsync(() => null);

        _unitOfWorkMock.Setup(
            repo => repo.UserRepository.CreateAsync(It.IsAny<UserEntity>())
        ).ReturnsAsync(userEntity);

        // act
        var result = await _CreateUsersCommandHandler.Handle(userToCreateCommand, new CancellationToken());

        //assert
        Assert.True(result.Success);
    }

    [Fact(DisplayName = "Create Invalid User Return Success False")]
    public async void Create_WhenIsInvalid_ReturnSuccessFalse()
    {
        // Arrange
        var userToCreateCommand = CreateUsersCommandFixture.CreateInvalid_CreateUsersCommand();
        var userEntity = _mapper.Map<UserEntity>(userToCreateCommand);

        _unitOfWorkMock.Setup(
            repo => repo.UserRepository.GetAsync(It.IsAny<Expression<Func<UserEntity, bool>>>(), true)
        ).ReturnsAsync(() => null);
        
        // Act
        var result = await _CreateUsersCommandHandler.Handle(userToCreateCommand, new CancellationToken());

        // Assert
        Assert.False(result.Success);
    }

    [Fact(DisplayName = "Create Exists User Return Success False")]
    public async void Create_WhenIsExists_ReturnUser()
    {
        // Arrange
        var userToCreateCommand = CreateUsersCommandFixture.CreateInvalid_CreateUsersCommand();
        var userEntity = _mapper.Map<UserEntity>(userToCreateCommand);
        var resultList = new List<UserEntity>
        {
            userEntity
        };

        _unitOfWorkMock.Setup(
            repo => repo.UserRepository.GetAsync(It.IsAny<Expression<Func<UserEntity, bool>>>(), true)
        ).ReturnsAsync(resultList);

        // Act
        var result = await _CreateUsersCommandHandler.Handle(userToCreateCommand, new CancellationToken());

        // Assert
        Assert.False(result.Success);
    }


}