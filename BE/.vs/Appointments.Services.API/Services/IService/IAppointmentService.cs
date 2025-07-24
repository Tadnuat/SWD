using Appointments.Services.API.Models;
using Appointments.Services.API.Models.Dtos;
using Appointments.Services.API.Models.DTOs;    

namespace Appointments.Services.API.Services.IService
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointmentAsync(CreateAppointmentDTO dto);
        Task<bool> UpdateConfirmedStatusAsync(int id);
        Task<bool> UpdateCancelledStatusAsync(int id);
        Task<bool> UpdateCompletedStatusAsync(int id);
        Task<bool> DeleteStatusAsync(int id);
        Task<Appointment?> UpdateAppointmentAsync(int id, UpdateAppointmentDTO dto);


    }
} 