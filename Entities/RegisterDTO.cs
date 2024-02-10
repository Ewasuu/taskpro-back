using System.ComponentModel.DataAnnotations;

namespace TaskPro_back.Entities
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "El nombre es demasiado largo, el maximo es de 200 caracteres")]
        public string UserName {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
