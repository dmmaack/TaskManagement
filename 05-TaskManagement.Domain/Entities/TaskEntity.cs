using System.ComponentModel.DataAnnotations;
using TaskManagement.Core.Enums;

namespace TaskManagement.Domain.Entities;

public class TaskEntity : BaseEntity
{
    public TaskEntity(long id, string title, string description,
        DateTime startDate, DateTime endDate, DateTime registerDate,
        int status, int priority, long userId, long assignedTo) : base(id)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        RegisterDate = registerDate;
        Status = status;
        Priority = priority;
        UserId = userId;
        AssignedTo = assignedTo;
    }

    public TaskEntity() { }

    [Required(ErrorMessage = "O titulo da tarefa não pode ser vazio.")]
    [MinLength(3, ErrorMessage = "O titulo da tarefa deve ter no mínimo 3 caracteres.")]
    [MaxLength(80, ErrorMessage = "O titulo da tarefa deve ter no máximo 80 caracteres.")] 
    public string Title { get; private set; }

    [MaxLength(5000, ErrorMessage = "A descrição da tarefa deve ter no máximo 5000 caracteres.")] 
    public string Description { get; private set; }
    
    [DataType(DataType.Date)]
    public DateTime StartDate { get; private set; }

    [DataType(DataType.Date)]
    public DateTime EndDate { get; private set; }

    [DataType(DataType.Date)]
    public DateTime RegisterDate { get; private set; } = DateTime.UtcNow;

    public int Status { get; private set; } = (int)StatusEnum.Pending;
    public int Priority { get; private set; } = (int)PriorityTaskEnum.Low;

    [Required(ErrorMessage = "É necessário informar um Usuario criador da Tarefa.")]
    public long UserId { get; private set; }

    public long AssignedTo { get; private set; }

    public UserEntity User { get; private set; }
    public UserEntity UserAssigned { get; private set; }


    public void SetTaskId(long taskId) => Id = taskId;

    public void ValidateUser()
    {
        this.Validate(this);
    }


}