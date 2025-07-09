using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Services.API.Models;
using Users.Services.API.Models.Dtos;
using Users.Services.API.Services.IService;

namespace Users.Services.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAll()
        {
            return Ok(await _reviewService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetById(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> Create([FromBody] ReviewDTO dto)
        {
            var review = await _reviewService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Review>> Update(int id, [FromBody] ReviewDTO dto)
        {
            var review = await _reviewService.UpdateAsync(id, dto);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _reviewService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
} 