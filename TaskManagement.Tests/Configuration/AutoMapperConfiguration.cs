using AutoMapper;
using TaskManagement.Application.Commands.TasksCommands.CreateTasksCommand;
using TaskManagement.Application.Commands.TasksCommands.UpdateTasksCommand;
using TaskManagement.Application.Commands.UsersCommands.CreateUsersCommand;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities;

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

        });

        return autoMapperConfig.CreateMapper();
    }
}