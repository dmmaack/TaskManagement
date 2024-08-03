using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Entities.Commands.UsersCommands.CreateUsers;

namespace TaskManagement.Domain.Interfaces.Services;

public interface ICreateUserServices
{
    Task<NotificationResult<BaseUserDTO>> Handle(UserEntity request);
    Task<NotificationResult<BaseUserDTO>> Handle(ProducerUsersRabbitMQCommand request);
}