namespace Users.Services.API.Models.Dtos
{
    public class UserWithLawyerProfileDTO
    {
        public UserDTO User { get; set; }
        public LawyerProfileDTO? LawyerProfile { get; set; }

    }
}
