using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Services.API.Models;
using Users.Services.API.Models.Dtos;

namespace Users.Services.API.Services.IService
{
    public interface IFormService
    {
        Task<IEnumerable<Form>> GetAllAsync();
        Task<Form?> GetByIdAsync(int id);
        Task<Form> CreateAsync(FormDTO dto);
        Task<Form?> UpdateAsync(int id, FormDTO dto);
        Task<bool> DeleteAsync(int id);
    }
} 