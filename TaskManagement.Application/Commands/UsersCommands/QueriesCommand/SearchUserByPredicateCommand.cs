using MediatR;
using TaskManagement.Core.Comunications.Messages.Notifications;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Application.Commands.UsersCommands.QueriesCommand;

public class SearchUserByPredicateCommand : IRequest<NotificationResult<IEnumerable<BaseUserDTO>>>
{
    public string Search { get; set; }
}