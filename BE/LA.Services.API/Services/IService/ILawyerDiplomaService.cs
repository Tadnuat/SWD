using LA.Services.API.Models.Dtos;

namespace LA.Services.API.Services.IService
{
    public interface ILawyerDiplomaService
    {
        Task<ResponseDto<IEnumerable<LawyerDiplomaDTO>>> GetAllDiplomasAsync(bool includeDeleted = false);
        Task<ResponseDto<LawyerDiplomaDTO>> GetDiplomaByIdAsync(int id);
        Task<ResponseDto<IEnumerable<LawyerDiplomaDTO>>> GetDiplomasByLawyerIdAsync(int lawyerId, bool includeDeleted = false);
        Task<ResponseDto<LawyerDiplomaDTO>> CreateDiplomaAsync(LawyerDiplomaDTO diplomaDto);
        Task<ResponseDto<LawyerDiplomaDTO>> UpdateDiplomaAsync(int id, LawyerDiplomaDTO diplomaDto);
        Task<ResponseDto<bool>> DeleteDiplomaAsync(int id);
    }
} 