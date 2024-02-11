using System.ComponentModel.DataAnnotations;

namespace TaskPro_back.Entities
{
    public class TaskDTO
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
