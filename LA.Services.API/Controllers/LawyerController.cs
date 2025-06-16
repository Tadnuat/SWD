using AutoMapper;
using LA.Services.API.Models.Dtos;
using LA.Services.API.Services;
using LA.Services.API.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace LA.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerController : ControllerBase
    {
        private readonly ILawyerService _service;
        private readonly IMapper _mapper;

        public LawyerController(ILawyerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("GetAllLawyerProfile")]
        public async Task<ActionResult<ResponseDto<IEnumerable<LawyerProfileDTO>>>> GetAll()
        {
            var response = new ResponseDto<IEnumerable<LawyerProfileDTO>>();

            try
            {
                var lawyers = await _service.GetAllLawyersAsync();

                var dtoList = _mapper.Map<IEnumerable<LawyerProfileDTO>>(lawyers);
                response.Result = dtoList;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        // GET: api/lawyer/GetProfileById/5
        [HttpGet("GetProfileById/{id}")]
        public async Task<ActionResult<ResponseDto<LawyerProfileDTO>>> GetById(int id)
        {
            var response = new ResponseDto<LawyerProfileDTO>();

            try
            {
                var profile = await _service.GetLawyerByIdAsync(id);
                if (profile == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No profile found with id = {id}";
                    return NotFound(response);
                }

                response.Result = profile;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }

            return Ok(response);
        }

        // POST: api/lawyer/CreateLawyerProfile
        //[HttpPost("CreateLawyerProfile")]
        //public async Task<ActionResult<ResponseDto<LawyerProfileDTO>>> CreateLawyerProfile([FromBody] LawyerProfileDTO dto)
        //{
        //    var response = new ResponseDto<LawyerProfileDTO>();

        //    try
        //    {
        //        var created = await _service.CreateLawyerAsync(dto);
        //        response.Result = created;
        //        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.Message = ex.Message;
        //        return StatusCode(500, response);
        //    }
        //}

        // PUT: api/lawyer/UpdateLawyerProfile/5
        [HttpPut("UpdateLawyerProfile/{id}")]
        public async Task<IActionResult> UpdateLawyerProfile(int id, [FromBody] LawyerProfileDTO dto)
        {
            var response = new ResponseDto<bool>();

            try
            {
                var success = await _service.UpdateLawyerAsync(id, dto);
                if (!success)
                {
                    response.IsSuccess = false;
                    response.Message = $"No lawyer profile found with id = {id}";
                    return NotFound(response);
                }

                response.Result = true;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
        }

        // DELETE: api/lawyer/DeleteLawyerProfile/5
        //[HttpDelete("DeleteLawyerProfile/{id}")]
        //public async Task<IActionResult> DeleteLawyerProfile(int id)
        //{
        //    var response = new ResponseDto<bool>();

        //    try
        //    {
        //        var success = await _service.DeleteLawyerAsync(id);
        //        if (!success)
        //        {
        //            response.IsSuccess = false;
        //            response.Message = $"No lawyer profile found with id = {id}";
        //            return NotFound(response);
        //        }

        //        response.Result = true;
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.Message = ex.Message;
        //        return StatusCode(500, response);
        //    }
        //}
        [HttpGet("GetProfileByUserId/{userId}")]
        public async Task<ActionResult<ResponseDto<LawyerProfileDTO>>> GetProfileByUserId(int userId)
        {
            var response = new ResponseDto<LawyerProfileDTO>();
            try
            {
                var profile = await _service.GetLawyerByUserIdAsync(userId);
                if (profile == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No profile found with userId = {userId}";
                    return NotFound(response);
                }
                response.Result = profile;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return StatusCode(500, response);
            }
            return Ok(response);
        }
        //UPDATE
        [HttpPut("UpdateLawyerByLaywerId/{id}")]
        public async Task<IActionResult> UpdateLawyer(int id, [FromBody] UpdateLawyerDTO dto)
        {
            var updated = await _service.UpdateLawyerProfileAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
    }
    }
