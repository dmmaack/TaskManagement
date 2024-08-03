using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using TaskManagement.Domain.Configurations;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interfaces.RabbitMQ;

namespace TaskManagement.Application.Services.RabbitMQ;

public class RabbitMQProducerService : BaseRabbitMQService, IRabbitMQProducer
{
    private readonly RabbitMqConfiguration _configuration;
    public RabbitMQProducerService(IOptions<RabbitMqConfiguration> option) : base(option)
    {
        _configuration = option.Value;
    }

    public Task SendProductMessage<T>(T message)
    {
        using var channel = this.CreateConnection();

        this.CreateExchangeQueue(channel);

        // serialize message
        var json = JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(json);

        // send the data to queue
        channel.BasicPublish(exchange: _configuration.Queues.TaskManagementExchange, 
                             routingKey: _configuration.Queues.CreateUsersCommandRoutingKey, 
                             body: body);

        return Task.CompletedTask;
    }
}