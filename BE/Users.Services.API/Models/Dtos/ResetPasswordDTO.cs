namespace Users.Services.API.Models.Dtos
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string Otp { get; set; }
        public string NewPassword { get; set; }
    }
} 