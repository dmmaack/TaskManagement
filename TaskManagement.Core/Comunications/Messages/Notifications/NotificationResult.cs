namespace TaskManagement.Core.Comunications.Messages.Notifications;

public class NotificationResult<T>
{
    public NotificationResult()
    {
    }

    public NotificationResult(bool success, DomainNotification notification, T data)
    {
        Success = success;
        Notification = notification;
        Data = data;
    }

    public bool Success { get; private set; }
    public DomainNotification Notification { get; private set; }
    public T Data { get; private set; }
}