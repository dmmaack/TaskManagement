using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TaskManagement.Domain.Configurations;
using TaskManagement.Domain.Entities.Commands.UsersCommands.CreateUsers;
using TaskManagement.Domain.Interfaces.RabbitMQ;
using TaskManagement.Domain.Interfaces.Services;

namespace TaskManagement.Application.Services.RabbitMQ;

public class RabbitMQConsumerService : BaseRabbitMQService, IRabbitMQConsumer
{
    private readonly RabbitMqConfiguration _configuration;
    private readonly ILogger<RabbitMQConsumerService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public RabbitMQConsumerService(IOptions<RabbitMqConfiguration> option, 
        ILogger<RabbitMQConsumerService> logger, 
        IServiceScopeFactory scopeFactory) : base(option)
    {
        _configuration = option.Value;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task ReceiveProductMessage()
    {
        using var channel = this.CreateConnection();

        this.CreateExchangeQueue(channel);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (sender, eventArgs) => 
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var objectDTO = JsonConvert.DeserializeObject<ProducerUsersRabbitMQCommand>(message);

            _logger.LogInformation("Recebido Cliente {email} para cadastro na Base de Dados.", objectDTO.Email);

            if (objectDTO is null || objectDTO.Email is null)
            {
                _logger.LogError("Objeto Cliente nulo");
                channel.BasicNack(eventArgs.DeliveryTag, false, false);
            }

            using var scope = _scopeFactory.CreateScope();

            var createUserService = scope.ServiceProvider.GetRequiredService<ICreateUserServices>();
            
            var notificationResult = await createUserService.Handle(objectDTO);

            if (notificationResult.Success)
                channel.BasicAck(eventArgs.DeliveryTag, false);
            else
                channel.BasicNack(eventArgs.DeliveryTag, false, false);

        };

        
        await Task.Run(
                    () => channel.BasicConsume(queue: _configuration.Queues.CreateUserQueue, false, consumer),
                    new CancellationToken()
                );

        Console.WriteLine($" [x] Filas em processamento");
        Console.ReadLine();
    }

}