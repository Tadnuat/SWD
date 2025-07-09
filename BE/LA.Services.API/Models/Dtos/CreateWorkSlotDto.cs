using System.ComponentModel.DataAnnotations;

namespace LA.Services.API.Models.Dtos
{
    public class CreateWorkSlotDto
    {
        [Required]
        public string DayOfWeek { get; set; }

        [Required]
        public string Slot { get; set; }

        public bool IsActive { get; set; }
    }
} 