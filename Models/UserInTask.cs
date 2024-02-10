using System.ComponentModel.DataAnnotations;

namespace TaskPro_back.Models
{
    public class UserInTask
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [Key]
        public Guid TaskId { get; set; }
        public Task Task { get; set; }

        public Roles Role { get; set; }
    }
}
