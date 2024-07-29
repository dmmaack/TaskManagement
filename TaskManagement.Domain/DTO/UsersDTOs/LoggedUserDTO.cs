using TaskManagement.Core.Enums;

namespace TaskManagement.Domain.DTO.UsersDTOs;

public class LoggedUserDTO
{
    public LoggedUserDTO() { }

    public LoggedUserDTO(long id, string name, int userRule)
    {
        Id = id;
        Name = name;
        UserRule = userRule;
    }

    public long Id { get; private set; }
    public string Name { get; private set; }
    public int UserRule { get; private set; }

    public UserRulesEnum GetRule { get { return (UserRulesEnum)UserRule; } }
}