using System.ComponentModel.DataAnnotations;

namespace LA.Services.API.Models.Dtos
{
    public class ActivateWorkSlotDto
    {
        [Required]
        public string Slot { get; set; }
        [Required]
        public string DayOfWeek { get; set; }
        [Required]
        public int LawyerId { get; set; }
    }
} 