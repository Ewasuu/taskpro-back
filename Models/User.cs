using System.ComponentModel.DataAnnotations;

namespace TaskPro_back.Models
{
    public class User
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        public virtual ICollection<UserInTask> UsersInTasks { get; set; }
    }
}
