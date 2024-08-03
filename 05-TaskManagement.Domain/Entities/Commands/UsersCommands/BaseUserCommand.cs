using MediatR;
using TaskManagement.Domain.Commands.Validators;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Domain.Entities.Commands.UsersCommands;

public class BaseUserCommand : BaseValidations, IRequest<BaseUserDTO>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
    public string Password { get; set; }
    public int UserRule { get; private set; }
}