using System.ComponentModel.DataAnnotations;

namespace TaskPro_back.Entities
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
    }
}
