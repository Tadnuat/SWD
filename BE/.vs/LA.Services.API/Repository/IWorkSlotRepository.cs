using System.Collections.Generic;
using System.Threading.Tasks;
using LA.Services.API.Models;

namespace LA.Services.API.Repository
{
    public interface IWorkSlotRepository
    {
        Task<IEnumerable<WorkSlot>> GetAllAsync();
        Task<WorkSlot?> GetByIdAsync(int id);
        Task AddAsync(WorkSlot workSlot);
        Task UpdateAsync(WorkSlot workSlot);
        Task DeleteAsync(int id);
    }
} 