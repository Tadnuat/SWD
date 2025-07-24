using System.Collections.Generic;
using System.Threading.Tasks;
using LA.Services.API.Models;

namespace LA.Services.API.Repository
{
    public interface ILawyerProfileRepository
    {
        Task<IEnumerable<LawyerProfile>> GetAllAsync();
        Task<LawyerProfile?> GetByIdAsync(int id);
        Task AddAsync(LawyerProfile profile);
        Task UpdateAsync(LawyerProfile profile);
        Task DeleteAsync(int id);
    }
} 