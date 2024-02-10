using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskPro_back.Models
{
    public class Task
    {
        [Key]
        public Guid id { get; set; }
        
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set;} = DateTime.Now;
    }
}
