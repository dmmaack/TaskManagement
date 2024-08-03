namespace TaskManagement.Domain.Configurations;

public class RabbitMqConfiguration
    {
        public string Host { get; set; } = string.Empty;
        public RabbitMqQueuesConfiguration Queues { get; set; } = null!;
    }

    public class RabbitMqQueuesConfiguration
    {
        public string CreateUsersCommandRoutingKey { get; set; } = string.Empty;
        public string CreateUserQueue { get; set; } = string.Empty;
        public string TaskManagementExchange { get; set; } = string.Empty;
    }