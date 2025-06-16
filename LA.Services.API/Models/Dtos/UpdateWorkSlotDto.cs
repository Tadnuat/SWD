using System.ComponentModel.DataAnnotations;

namespace LA.Services.API.Models.Dtos
{
    public class UpdateWorkSlotDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string DayOfWeek { get; set; }

        [Required]
        public string Slot { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
} 