namespace TaskManagement.Domain.DTO.UsersDTOs
{
    public class BaseUserDTO
    {
        public BaseUserDTO(long id, string name, string email, 
            string userName, DateTime registerDate, bool isActive)
        {
            Id = id;
            Name = name;
            Email = email;
            UserName = userName;
            RegisterDate = registerDate;
            IsActive = isActive;
        }
        public BaseUserDTO() { }

        public BaseUserDTO(long id, string email, DateTime registerDate) 
        {
            this.Id = id;
            this.Email = email;
            this.RegisterDate = registerDate;     
        }

        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string UserName { get; private set; }
        public DateTime RegisterDate { get; private set; }
        public bool IsActive { get; private set; }
    }
}