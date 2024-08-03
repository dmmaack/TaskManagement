namespace TaskManagement.Domain.DTO.TasksDTOs;

public class TaskWithUserDTO : BaseTaskDTO
{
    public TaskWithUserDTO(long id, string title, string description, DateTime startDate, 
        DateTime endDate, DateTime registerDate, int status, int priority, long userId, 
        string userCreatorName, long assignedTo, string assignedToUserName) 
        : base(id, title, startDate, endDate, registerDate, priority, description, status, userId, assignedTo)
    {
        UserCreatorName = userCreatorName;
        AssignedToUserName = assignedToUserName;
    }

    public TaskWithUserDTO() { }


    public string UserCreatorName { get; set; }
    public string AssignedToUserName { get; set; }


}