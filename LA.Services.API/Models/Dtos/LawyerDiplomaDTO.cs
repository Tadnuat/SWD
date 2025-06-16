namespace LA.Services.API.Models.Dtos
{
    public class LawyerDiplomaDTO
    {
        public int Id { get; set; }
        public int LawyerId { get; set; }
        public string Title { get; set; }
        public string QualificationType { get; set; }
        public string Description { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string IssuedBy { get; set; }
        public string DocumentUrl { get; set; }
        public bool IsPublic { get; set; }
        public bool IsVerified { get; set; }
    }
} 