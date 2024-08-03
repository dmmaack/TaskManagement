using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TaskManagement.Application.Services.RabbitMQ;
using TaskManagement.Application.Services.UsersServices;
using TaskManagement.Domain.Configurations;
using TaskManagement.Domain.DTO.TasksDTOs;
using TaskManagement.Domain.DTO.UsersDTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Entities.Commands.TasksCommands.CreateTasks;
using TaskManagement.Domain.Entities.Commands.TasksCommands.UpdateTasks;
using TaskManagement.Domain.Entities.Commands.UsersCommands.CreateUsers;
using TaskManagement.Domain.Interfaces.RabbitMQ;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Domain.Interfaces.Services;
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
        services.AddScoped<ICreateUserServices, CreateUserServices>();

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
                conf.CreateMap<UserEntity, ProducerUsersRabbitMQCommand>().ReverseMap();
                conf.CreateMap<BaseUserDTO, ProducerUsersRabbitMQCommand>().ReverseMap();
                conf.CreateMap<TaskEntity, BaseTaskDTO>().ReverseMap();
                conf.CreateMap<TaskEntity, CreateTasksCommand>().ReverseMap();
                conf.CreateMap<TaskEntity, UpdateTasksCommand>().ReverseMap();
            }
        );

        services.AddSingleton(autoMapperConfig.CreateMapper());

        services.AddLogging();


        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetSection("PrivateKey").Value);
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }

    public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfiguration>(configuration.GetSection("RabbitMqConfig"));
        services.AddScoped<IRabbitMQProducer, RabbitMQProducerService>();
        services.AddScoped<IRabbitMQConsumer, RabbitMQConsumerService>();
        //services.AddScoped<IRabbitMQConsumer>(x => new RabbitMQConsumer());

        return services;
    }

}