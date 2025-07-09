using LA.Services.API.Models;
using LA.Services.API.Models.Dtos;

namespace LA.Services.API.Services.IService
{
    public interface ILawyerService
    {
        Task<IEnumerable<LawyerProfileDTO>> GetAllLawyersAsync();
        Task<LawyerProfileDTO?> GetLawyerByIdAsync(int id);
        Task<LawyerProfileDTO> CreateLawyerAsync(LawyerProfileDTO profileDto);
        Task<LawyerProfileDTO?> GetLawyerByUserIdAsync(int userId);
        Task<bool> UpdateLawyerAsync(int id, LawyerProfileDTO profileDto);
        Task<bool> DeleteLawyerAsync(int id);

        Task<LawyerProfileDTO?> UpdateLawyerProfileAsync(int id, UpdateLawyerDTO dto);
    }
}
