using TaskPro_back.Enums;

namespace TaskPro_back.Entities
{
    public class AddUserInTaskDTO
    {
        public Guid UserId { get; set; }
        public Guid TaskId { get; set; }
        public Roles Role {  get; set; }
    }
}
