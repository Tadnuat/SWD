using LA.Services.API.Models.Dtos;

namespace LA.Services.API.Services.IService
{
    public interface IWorkSlotService
    {
        Task<IEnumerable<WorkSlotDto>> GetAllWorkSlotsAsync();
        Task<WorkSlotDto> GetWorkSlotByIdAsync(int id);
        Task<WorkSlotDto> CreateWorkSlotAsync(int lawyerId, CreateWorkSlotDto createWorkSlotDto);
        Task<WorkSlotDto> UpdateWorkSlotAsync(int lawyerId, UpdateWorkSlotDto updateWorkSlotDto);
        Task<bool> DeleteWorkSlotAsync(int id);
        Task<IEnumerable<WorkSlotDto>> GetWorkSlotsByLawyerIdAsync(int lawyerId);
        Task<bool> DeactivateWorkSlotAsync(DeactivateWorkSlotDto dto);
        Task<bool> ActivateWorkSlotAsync(ActivateWorkSlotDto dto);
    }
} 