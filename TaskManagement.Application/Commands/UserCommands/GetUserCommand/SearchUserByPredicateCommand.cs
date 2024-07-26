using MediatR;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Application.Commands.UserCommands.GetUserCommand;

public class SearchUserByPredicateCommand : IRequest<BaseUserDTO>
{
    public string Predicate { get; set; }

    public SearchUserByPredicateCommand(string predicate)
    {
        Predicate = predicate;
    }
}