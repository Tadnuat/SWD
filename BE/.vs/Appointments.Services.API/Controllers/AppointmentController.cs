using Appointments.Services.API.Models;
using Appointments.Services.API.Models.Dtos;
using Appointments.Services.API.Services;
using Appointments.Services.API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Services.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        //[HttpPost]
        //public async Task<ActionResult<ResponseDto<Appointment>>> CreateAppointment([FromBody] CreateAppointmentDTO dto)
        //{
        //    var response = new ResponseDto<Appointment>();

        //    try
        //    {
        //        var result = await _appointmentService.CreateAppointmentAsync(dto);
        //        response.Result = result;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.DisplayMessage = ex.Message;
        //        return StatusCode(500, response);
        //    }

        //    return Ok(response);
        //}
        [HttpPost("CREATE")]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appointment = await _appointmentService.CreateAppointmentAsync(dto);
            return Ok(appointment);
        }

        [HttpPut("{id}/confirm")]
        public async Task<IActionResult> UpdateConfirmedStatus(int id)
        {
            var result = await _appointmentService.UpdateConfirmedStatusAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> UpdateCancelledStatus(int id)
        {
            var result = await _appointmentService.UpdateCancelledStatusAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<IActionResult> UpdateCompletedStatus(int id)
        {
            var result = await _appointmentService.UpdateCompletedStatusAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var result = await _appointmentService.DeleteStatusAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDTO dto)
        {
            var updated = await _appointmentService.UpdateAppointmentAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

    }
}
