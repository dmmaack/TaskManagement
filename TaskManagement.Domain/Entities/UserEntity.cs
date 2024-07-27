using System.ComponentModel.DataAnnotations;
using TaskManagement.Core.Enums;

namespace TaskManagement.Domain.Entities;

public class UserEntity : BaseEntity
{
    [Required(ErrorMessage = "O nome não pode ser vazio.")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
    [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]  
    public string Name { get; private set; }

    [Required(ErrorMessage = "O email não pode ser vazio.")]
    [MinLength(10, ErrorMessage = "O email deve ter no mínimo 10 caracteres.")]
    [MaxLength(180, ErrorMessage = "O email deve ter no máximo 180 caracteres.")]
    [EmailAddress(ErrorMessage = "O email informado não é válido.")]
    public string Email { get; private set; }

    [Required(ErrorMessage = "O Nome de Usuário não pode ser vazio.")]
    [MinLength(3, ErrorMessage = "O Nome de Usuário deve ter no mínimo 3 caracteres.")]
    [MaxLength(12, ErrorMessage = "O Nome de Usuário deve ter no máximo 12 caracteres.")]
    public string UserName { get; private set; }

    [Required(ErrorMessage = "A senha não pode ser vazia.")]
    public string Password { get; private set; }

    public DateTime RegisterDate { get; private set; } = DateTime.UtcNow;

    public bool? IsActive { get; private set; } = true;

    public int UserRule { get; set; } = (int)UserRulesEnum.NoPermission;
    

    public IEnumerable<TaskEntity> Tasks { get; set; }

    public IEnumerable<TaskEntity> TasksAssigned { get; set; }

    public UserEntity() { }

    public UserEntity(int id, string name, string email, string userName, string password,
                      DateTime registerDate, bool isActive, int userRule)
                      : base(id)
    {
        Name = name;
        Email = email;
        UserName = userName;
        Password = password;
        RegisterDate = registerDate;
        IsActive = isActive;
        UserRule = userRule;

        Tasks = new HashSet<TaskEntity>();
        TasksAssigned = new HashSet<TaskEntity>();
    }

    public void SetPassword(string password) => Password = password;
    public void SetIsActive(bool isActive) => IsActive = isActive;

    public void SetUserId(long id) => Id = id;

    public void ValidateUser()
    {
        this.Validate(this);
    }

    
}