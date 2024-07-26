using MediatR;
using TaskManagement.Domain.DTO.UsersDTOs;

namespace TaskManagement.Application.Commands.UserCommands;

public class BaseUserCommand : IRequest<BaseUserDTO>
{
    public BaseUserCommand(string name, string email, string userName, DateTime registerDate, bool isActive, string password)
    {
        Name = name;
        Email = email;
        UserName = userName;
        RegisterDate = registerDate;
        IsActive = isActive;
        Password = password;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
    public string Password { get; set; }
}