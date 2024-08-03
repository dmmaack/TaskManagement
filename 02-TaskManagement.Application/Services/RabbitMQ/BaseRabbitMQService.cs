using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using RabbitMQ.Client;
using TaskManagement.Domain.Configurations;

namespace TaskManagement.Application.Services.RabbitMQ;

public class BaseRabbitMQService
{
    private readonly RabbitMqConfiguration _configuration;

    public BaseRabbitMQService(IOptions<RabbitMqConfiguration> option)
    {
        _configuration = option.Value;
    }

    protected IModel CreateConnection()
    {
        var factory = new ConnectionFactory { HostName = _configuration.Host };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();
        
        return channel;
    }

    protected void CreateExchangeQueue(IModel channel)
    {
        channel.ExchangeDeclare(exchange: _configuration.Queues.TaskManagementExchange, ExchangeType.Direct);

        // declare the queue after mentioning name and a few property relatted
        channel.QueueDeclare(queue: _configuration.Queues.CreateUserQueue, durable: true, exclusive: false, autoDelete: false);

        channel.QueueBind(queue: _configuration.Queues.CreateUserQueue,
                          exchange: _configuration.Queues.TaskManagementExchange,
                          routingKey: _configuration.Queues.CreateUsersCommandRoutingKey);
        
    }
}