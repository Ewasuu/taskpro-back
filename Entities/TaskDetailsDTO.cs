
namespace TaskPro_back.Entities
{
    public class TaskDetailDTO : Models.Task
    {
        public List<UserDTO> Users { get; set; }
    }
}
