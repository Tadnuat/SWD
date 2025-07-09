using Users.Services.API.Models.Dtos;

namespace Users.Services.API.Services.IService
{
    public interface IUserWithLawyerProfileService
    {
        Task<IEnumerable<UserWithLawyerProfileDTO>> GetAllUsersWithLawyerProfileAsync();
        Task<UserWithLawyerProfileDTO?> GetUsersWithLawyerProfileByIdAsync(int userId);
        Task<IEnumerable<UserWithLawyerProfileDTO>> GetUsersWithLawyerProfileOnlyAsync();
    }
}
