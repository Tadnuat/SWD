using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Services.API.Models;
using Users.Services.API.Models.Dtos;

namespace Users.Services.API.Services.IService
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task<Review> CreateAsync(ReviewDTO dto);
        Task<Review?> UpdateAsync(int id, ReviewDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
