using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Services.API.Models;

namespace Users.Services.API.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task AddAsync(Review review);
        Task UpdateAsync(Review review);
        Task DeleteAsync(int id);
    }
} 