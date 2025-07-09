using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Services.API.Data;
using Users.Services.API.Models;
using Users.Services.API.Models.Dtos;
using Users.Services.API.Services.IService;

namespace Users.Services.API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly UserDbContext _context;
        public ReviewService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FindAsync(id);
        }

        public async Task<Review> CreateAsync(ReviewDTO dto)
        {
            var review = new Review
            {
                LawyerId = dto.LawyerId,
                UserId = dto.UserId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<Review?> UpdateAsync(int id, ReviewDTO dto)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return null;
            review.LawyerId = dto.LawyerId;
            review.UserId = dto.UserId;
            review.Rating = dto.Rating;
            review.Comment = dto.Comment;
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 