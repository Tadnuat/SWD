using Microsoft.AspNetCore.Mvc;
using LA.Services.API.Services.IService;
using LA.Services.API.Models.Dtos;

namespace LA.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerDiplomaController : ControllerBase
    {
        private readonly ILawyerDiplomaService _diplomaService;

        public LawyerDiplomaController(ILawyerDiplomaService diplomaService)
        {
            _diplomaService = diplomaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiplomas([FromQuery] bool includeDeleted = false)
        {
            var response = await _diplomaService.GetAllDiplomasAsync(includeDeleted);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiplomaById(int id)
        {
            var response = await _diplomaService.GetDiplomaByIdAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("lawyer/{lawyerId}")]
        public async Task<IActionResult> GetDiplomasByLawyerId(int lawyerId, [FromQuery] bool includeDeleted = false)
        {
            var response = await _diplomaService.GetDiplomasByLawyerIdAsync(lawyerId, includeDeleted);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiploma([FromBody] LawyerDiplomaDTO diplomaDto)
        {
            var response = await _diplomaService.CreateDiplomaAsync(diplomaDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(GetDiplomaById), new { id = response.Result.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDiploma(int id, [FromBody] LawyerDiplomaDTO diplomaDto)
        {
            var response = await _diplomaService.UpdateDiplomaAsync(id, diplomaDto);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiploma(int id)
        {
            var response = await _diplomaService.DeleteDiplomaAsync(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
} 