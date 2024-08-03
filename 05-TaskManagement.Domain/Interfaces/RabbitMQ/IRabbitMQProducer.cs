namespace TaskManagement.Domain.Interfaces.RabbitMQ;

public interface IRabbitMQProducer
{
    public Task SendProductMessage<T>(T message);
}