namespace Users.Services.API.Models.Dtos
{
    public class ResponseDto<T>
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T Result { get; set; }
    }
}
