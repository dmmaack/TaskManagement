using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TaskManagement.CrossCutting.AppDependencies;
using TaskManagement.Domain.Interfaces.RabbitMQ;

public class Program
{
    public static IConfigurationRoot _configuration;

    public static void Main(string[] args)
    {

        var serviceCollection = new ServiceCollection();

        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


        // Set up configuration sources.
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(AppContext.BaseDirectory))
            .AddJsonFile("appsettings.json", optional: true);

        if (environment == "Development")
        {

            builder
                .AddJsonFile(
                    Path.Combine(AppContext.BaseDirectory, string.Format("..{0}..{0}..{0}", Path.DirectorySeparatorChar), $"appsettings.{environment}.json"),
                    optional: true
                );
        }
        else
        {
            builder
                .AddJsonFile($"appsettings.{environment}.json", optional: false);
        }        

        _configuration = builder.Build();

        serviceCollection.AddInfra(_configuration).AddRabbitMQ(_configuration);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var rabbitMQConsumer = serviceProvider.GetService<IRabbitMQConsumer>();
        rabbitMQConsumer.ReceiveProductMessage();

        // ReceiveMessage();
        Console.ReadLine();
        
    }

    public static void ReceiveMessage()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        var connection = factory.CreateConnection();

        var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "TaskManagement_EX", ExchangeType.Direct);

        // declare the queue after mentioning name and a few property relatted
        channel.QueueDeclare(queue: "CreateUser_Queue", durable: true, exclusive: false, autoDelete: false);

        channel.QueueBind(queue: "CreateUser_Queue",
                        exchange: "TaskManagement_EX",
                        routingKey: "CreateUsersCommand_RK");

        var consumer =  new EventingBasicConsumer(channel);

        consumer.Received += (sender, args) => 
        {
            var body = args.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($" [x] Received {message}");
        };

        channel.BasicConsume(queue: "CreateUser_Queue", false, consumer);

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }

}