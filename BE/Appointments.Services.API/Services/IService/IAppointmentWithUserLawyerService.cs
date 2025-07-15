﻿using Appointments.Services.API.Models.Dtos;
using Appointments.Services.API.Models;

namespace Appointments.Services.API.Services.IService
{
    public interface IAppointmentWithUserLawyerService
    {
        Task<IEnumerable<AppointmentWithUserLawyerDTO>> GetAllAppointmentsWithUserLawyerAsync();
        Task<AppointmentWithUserLawyerDTO?> GetAppointmentWithUserLawyerByIdAsync(int id);
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<AppointmentWithUserLawyerDTO?> UpdateAppointmentAsync(int id, Appointment updatedAppointment);
        Task<bool> RestoreAppointmentAsync(int id);
        Task<bool> SoftDeleteAppointmentAsync(int id);
        Task<IEnumerable<AppointmentWithUserLawyerDTO>> GetDeletedAppointmentsWithUserLawyerAsync();

        // Lấy danh sách lịch hẹn theo LawyerId
        Task<IEnumerable<AppointmentWithUserLawyerDTO>> GetAppointmentsByLawyerIdAsync(int lawyerId);

        // Lấy danh sách lịch hẹn theo UserId
        Task<IEnumerable<AppointmentWithUserLawyerDTO>> GetAppointmentsByUserIdAsync(int userId);
    }
}
