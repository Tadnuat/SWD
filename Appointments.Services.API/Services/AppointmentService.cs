using Appointments.Services.API.Data;
using Appointments.Services.API.Models;
using Appointments.Services.API.Models.DTOs;
using Appointments.Services.API.Models.Enums;
using Appointments.Services.API.Services.IService;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Appointments.Services.API.Models.Dtos;

namespace Appointments.Services.API.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppointmentDbContext _context;

        public AppointmentService(AppointmentDbContext context)
        {
            _context = context;
        }
        public async Task<Appointment> CreateAppointmentAsync(CreateAppointmentDTO dto)
        {   
            var appointment = new Appointment
            {
                UserId = dto.UserId,
                LawyerId = dto.LawyerId,
                ScheduledAt = dto.ScheduledAt,
                Slot = dto.Slot,
                CreateAt = DateTime.UtcNow,
                Spec = dto.Spec,
                Services = dto.Services ?? new List<string>(),

                // Gán mặc định
                Status = AppointmentStatus.Pending,
                IsDel = false,
                Note = dto.Note
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment;
        }

        public async Task<bool> UpdateConfirmedStatusAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            appointment.Status = AppointmentStatus.Confirmed;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCancelledStatusAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            appointment.Status = AppointmentStatus.Cancelled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCompletedStatusAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            appointment.Status = AppointmentStatus.Completed;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStatusAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return false;

            appointment.IsDel = true;
            await _context.SaveChangesAsync();
            return true;
        }
        //UPDATE
        public async Task<Appointment?> UpdateAppointmentAsync(int id, UpdateAppointmentDTO dto)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null || appointment.IsDel) return null;

            appointment.LawyerId = dto.LawyerId;
            appointment.ScheduledAt = dto.ScheduledAt;
            appointment.Slot = dto.Slot;
            appointment.Spec = dto.Spec;
            appointment.Services = dto.Services ?? new List<string>();
            appointment.Note = dto.Note;

            await _context.SaveChangesAsync();
            return appointment;
        }

    }
}
