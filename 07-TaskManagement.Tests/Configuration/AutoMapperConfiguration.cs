using AutoMapper;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Entities.Commands.TasksCommands.CreateTasks;
using TaskManagement.Domain.Entities.Commands.TasksCommands.UpdateTasks;
using TaskManagement.Domain.Entities.Commands.UsersCommands.CreateUsers;

namespace TaskManagement.Tests.Configuration;

public class AutoMapperConfiguration
{
    public static IMapper GetConfiguration()
    {
        var autoMapperConfig = new MapperConfiguration(conf =>
        {
            conf.CreateMap<UserEntity, BaseUserDTO>().ReverseMap();
            conf.CreateMap<UserEntity, CreateUsersCommand>().ReverseMap();
            conf.CreateMap<TaskEntity, BaseTaskDTO>().ReverseMap();
            conf.CreateMap<TaskEntity, CreateTasksCommand>().ReverseMap();
            conf.CreateMap<TaskEntity, UpdateTasksCommand>().ReverseMap();
            conf.CreateMap<BaseUserDTO, ProducerUsersRabbitMQCommand>().ReverseMap();
        });

        return autoMapperConfig.CreateMapper();
    }
}