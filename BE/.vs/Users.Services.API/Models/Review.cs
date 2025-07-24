using System;

namespace Users.Services.API.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int LawyerId { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 