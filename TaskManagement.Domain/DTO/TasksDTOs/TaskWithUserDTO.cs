namespace TaskManagement.Domain.DTO.TasksDTOs;

public class TaskWithUserDTO
{
    public TaskWithUserDTO(long id, string title, string description, 
        DateTime startDate, DateTime endDate, DateTime registerDate, 
        int status, int priority, long userId, string userCreator, 
        long assignedTo, string assignedToUser)
    {
        Id = id;
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        RegisterDate = registerDate;
        Status = status;
        Priority = priority;
        UserId = userId;
        UserCreator = userCreator;
        AssignedTo = assignedTo;
        AssignedToUser = assignedToUser;
    }

    public TaskWithUserDTO() { }

    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RegisterDate { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }

    public long UserId { get; set; }
    public string UserCreator { get; set; }

    public long AssignedTo { get; set; }
    public string AssignedToUser { get; set; }


}