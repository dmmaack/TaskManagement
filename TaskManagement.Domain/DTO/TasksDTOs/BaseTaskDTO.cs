namespace TaskManagement.Domain.DTO.TasksDTOs;

public class BaseTaskDTO
{
    public BaseTaskDTO(long id, string title, DateTime startDate, 
        DateTime registerDate, int priority, long assignedTo)
    {
        this.Id = id;
        this.Title = title;
        this.StartDate = startDate;
        this.RegisterDate = registerDate;
        this.Priority = priority;
        this.AssignedTo = assignedTo;
    }

    public BaseTaskDTO() { }

    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RegisterDate { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }
    public long UserId { get; set; }
    public long AssignedTo { get; set; }
}