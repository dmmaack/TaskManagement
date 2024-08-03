
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TaskManagement.Console.RabbitMQ;

public class ReceiveMessage
{
    public ReceiveMessage()
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
            //Console.WriteLine(" [*] Waiting for messages.");

        };

        channel.BasicConsume(queue: "CreateUser_Queue", false, consumer);
    }


}