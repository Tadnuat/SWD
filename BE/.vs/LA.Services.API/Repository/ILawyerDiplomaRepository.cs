using System.Collections.Generic;
using System.Threading.Tasks;
using LA.Services.API.Models;

namespace LA.Services.API.Repository
{
    public interface ILawyerDiplomaRepository
    {
        Task<IEnumerable<LawyerDiploma>> GetAllAsync();
        Task<LawyerDiploma?> GetByIdAsync(int id);
        Task AddAsync(LawyerDiploma diploma);
        Task UpdateAsync(LawyerDiploma diploma);
        Task DeleteAsync(int id);
    }
} 