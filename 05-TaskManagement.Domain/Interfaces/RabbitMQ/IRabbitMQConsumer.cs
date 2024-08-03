namespace TaskManagement.Domain.Interfaces.RabbitMQ;

public interface IRabbitMQConsumer
{
    public Task ReceiveProductMessage();
}