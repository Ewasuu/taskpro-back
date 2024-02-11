namespace TaskPro_back.Entities
{
    public class LoginResponseDTO<T> : ResponseDTO<T>
    {
        public string Token { get; set; }
    }
}
