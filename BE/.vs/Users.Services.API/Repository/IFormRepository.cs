using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Services.API.Models;

namespace Users.Services.API.Repository
{
    public interface IFormRepository
    {
        Task<IEnumerable<Form>> GetAllAsync();
        Task<Form?> GetByIdAsync(int id);
        Task AddAsync(Form form);
        Task UpdateAsync(Form form);
        Task DeleteAsync(int id);
    }
} 