using System.Net;
using System.Text.Json.Serialization;
using MediatR;

namespace TaskManagement.Core.Comunications.Messages.Notifications;

public class DomainNotification : Notification
{
    public string Message { get; private set; }

    [JsonIgnore]
    public HttpStatusCode StatusCode { get; private set; }
     
    public ICollection<string> Errors { get; private set; }

    public DomainNotification(string message, HttpStatusCode statusCode)
    {
        Message = message;
        StatusCode = statusCode;
    }
    public DomainNotification(string message, ICollection<string> errors, HttpStatusCode statusCode)
    {
        Message = message;
        Errors = errors;
        StatusCode = statusCode;
    }
}

public abstract class Notification : INotification
{
    public string Hash { get; private set; }

    public Notification()
    {
        Hash = Guid.NewGuid().ToString();
    }
}