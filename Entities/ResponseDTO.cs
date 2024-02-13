namespace TaskPro_back.Entities
{
    public class ResponseDTO<T>
    {
        public T Data { get; set; }

        public bool Success {  get; set; }

        public string ErrorMesage { get; set; }
    }
}
