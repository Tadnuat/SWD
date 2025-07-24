namespace Users.Services.API.Models.Dtos
{
    public class ReviewDTO
    {
        public int LawyerId { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        public string? Comment { get; set; }
    }
} 