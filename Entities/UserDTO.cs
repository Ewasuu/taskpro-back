using TaskPro_back.Enums;

namespace TaskPro_back.Entities
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Roles Role { get; set; }
    }
}
