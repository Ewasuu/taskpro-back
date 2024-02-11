using TaskPro_back.Enums;

namespace TaskPro_back.Entities
{
    public class UserTasksDTO : Models.Task
    {
        public Roles Role { get; set; }
    }
}
