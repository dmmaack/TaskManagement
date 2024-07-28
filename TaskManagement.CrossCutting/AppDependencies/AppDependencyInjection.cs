using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Commands.TasksCommands.CreateTasksCommand;
using TaskManagement.Application.Commands.TasksCommands.UpdateTasksCommand;
using TaskManagement.Application.Commands.UsersCommands.CreateUsersCommand;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Infra.Context;
using TaskManagement.Infra.Repositories;

namespace TaskManagement.CrossCutting.AppDependencies;

public static class AppDependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlConnection = configuration.GetConnectionString("SqlConnection");

        services.AddDbContext<AppDbContext>(options =>
                                            options.UseSqlServer(sqlConnection));

        //repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // MediatR
        var handlers = AppDomain.CurrentDomain.Load("TaskManagement.Application");
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(handlers);
        });

        var autoMapperConfig = new MapperConfiguration(conf => 
            {
                conf.CreateMap<UserEntity, BaseUserDTO>().ReverseMap();
                conf.CreateMap<UserEntity, CreateUsersCommand>().ReverseMap();
                conf.CreateMap<TaskEntity, BaseTaskDTO>().ReverseMap();
                conf.CreateMap<TaskEntity, CreateTasksCommand>().ReverseMap();
                conf.CreateMap<TaskEntity, UpdateTasksCommand>().ReverseMap();
            }
        );

        services.AddSingleton(autoMapperConfig.CreateMapper());


        return services;
    }
}