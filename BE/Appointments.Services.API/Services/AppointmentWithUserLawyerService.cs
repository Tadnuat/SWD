using Appointments.Services.API.Data;
using Appointments.Services.API.Models;
using Appointments.Services.API.Models.Dtos;
using Appointments.Services.API.Models.Enums;
using Microsoft.EntityFrameworkCore;



namespace Appointments.Services.API.Services
{
    public class AppointmentWithUserLawyerService
    {
        private readonly AppointmentDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public AppointmentWithUserLawyerService(
            AppointmentDbContext context,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<AppointmentWithUserLawyerDTO>> GetAllAppointmentsWithUserLawyerAsync()
        {
            var appointments = await _context.Appointments.Where(a => !a.IsDel).ToListAsync();
            var result = new List<AppointmentWithUserLawyerDTO>();

            var userClient = _httpClientFactory.CreateClient("UserService");

            foreach (var appointment in appointments)
            {
                // Lấy thông tin User (người đặt lịch)
                var userLawyerResponse = await userClient.GetAsync($"/api/UserWithLawyerProfile/{appointment.UserId}");
                UserWithLawyerProfileDTO? userWithLawyer = null;
                if (userLawyerResponse.IsSuccessStatusCode)
                {
                    var responseDto = await userLawyerResponse.Content.ReadFromJsonAsync<ResponseDto<UserWithLawyerProfileDTO>>();
                    userWithLawyer = responseDto?.Result;
                }

                // Lấy thông tin Lawyer (luật sư)
                var lawyerProfileResponse = await userClient.GetAsync($"/api/UserWithLawyerProfile/{appointment.LawyerId}");
                UserWithLawyerProfileDTO? lawyerWithProfile = null;
                if (lawyerProfileResponse.IsSuccessStatusCode)
                {
                    var responseDto = await lawyerProfileResponse.Content.ReadFromJsonAsync<ResponseDto<UserWithLawyerProfileDTO>>();
                    lawyerWithProfile = responseDto?.Result;
                }

                result.Add(new AppointmentWithUserLawyerDTO
                {
                    Id = appointment.Id,
                    UserId = appointment.UserId,
                    LawyerId = appointment.LawyerId,
                    ScheduledAt = appointment.ScheduledAt,
                    Slot = appointment.Slot,
                    CreateAt = appointment.CreateAt,
                    Status = appointment.Status,
                    IsDel = appointment.IsDel,
                    Note = appointment.Note,
                    Spec = appointment.Spec,
                    Services = appointment.Services,
                    User = userWithLawyer?.User,
                    LawyerProfile = lawyerWithProfile?.LawyerProfile // Gán profile của luật sư
                });
            }

            return result;
        }

        public async Task<AppointmentWithUserLawyerDTO?> GetAppointmentWithUserLawyerByIdAsync(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id && !a.IsDel);
            if (appointment == null)
                return null;

            var userClient = _httpClientFactory.CreateClient("UserService");

            // Lấy thông tin User (người đặt lịch)
            UserWithLawyerProfileDTO? userWithLawyer = null;
            var userLawyerResponse = await userClient.GetAsync($"/api/UserWithLawyerProfile/{appointment.UserId}");
            if (userLawyerResponse.IsSuccessStatusCode)
            {
                var responseDto = await userLawyerResponse.Content.ReadFromJsonAsync<ResponseDto<UserWithLawyerProfileDTO>>();
                userWithLawyer = responseDto?.Result;
            }

            // Lấy thông tin Lawyer (luật sư)
            UserWithLawyerProfileDTO? lawyerWithProfile = null;
            var lawyerProfileResponse = await userClient.GetAsync($"/api/UserWithLawyerProfile/{appointment.LawyerId}");
            if (lawyerProfileResponse.IsSuccessStatusCode)
            {
                var responseDto = await lawyerProfileResponse.Content.ReadFromJsonAsync<ResponseDto<UserWithLawyerProfileDTO>>();
                lawyerWithProfile = responseDto?.Result;
            }

            return new AppointmentWithUserLawyerDTO
            {
                Id = appointment.Id,
                UserId = appointment.UserId,
                LawyerId = appointment.LawyerId,
                ScheduledAt = appointment.ScheduledAt,
                Slot = appointment.Slot,
                CreateAt = appointment.CreateAt,
                Status = appointment.Status,
                IsDel = appointment.IsDel,
                Note = appointment.Note,
                Spec = appointment.Spec,
                Services = appointment.Services,
                User = userWithLawyer?.User,
                LawyerProfile = lawyerWithProfile?.LawyerProfile // Lấy đúng profile của luật sư
            };
        }
        // Tạo
        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            try
            {
                // Validate all required fields
                if (appointment == null)
                    throw new ArgumentNullException(nameof(appointment), "Appointment data cannot be null");

                // Validate UserId
                if (appointment.UserId <= 0)
                    throw new ArgumentException("UserId must be greater than 0");

                // Validate LawyerId
                if (appointment.LawyerId <= 0)
                    throw new ArgumentException("LawyerId must be greater than 0");

                // Validate ScheduledAt
                if (appointment.ScheduledAt == default)
                    throw new ArgumentException("ScheduledAt must be a valid date");

                // Validate Slot
                if (string.IsNullOrWhiteSpace(appointment.Slot))
                    throw new ArgumentException("Slot cannot be empty");

                // Validate Spec
                if (string.IsNullOrWhiteSpace(appointment.Spec))
                    throw new ArgumentException("Spec cannot be empty");

                // Create new appointment object to avoid any reference issues
                var newAppointment = new Appointment
                {
                    UserId = appointment.UserId,
                    LawyerId = appointment.LawyerId,
                    ScheduledAt = appointment.ScheduledAt,
                    Slot = appointment.Slot,
                    Spec = appointment.Spec,
                    Note = appointment.Note,
                    Status = appointment.Status == 0 ? AppointmentStatus.Pending : appointment.Status,
                    IsDel = false,
                    CreateAt = DateTime.Now,
                    Services = appointment.Services ?? new List<string>()
                };

                // Add to context
                _context.Appointments.Add(newAppointment);

                // Save changes
                await _context.SaveChangesAsync();

                return newAppointment;
            }
            catch (DbUpdateException dbEx)
            {
                // Log the database error
                throw new Exception($"Database error while creating appointment: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                // Log any other errors
                throw new Exception($"Error creating appointment: {ex.Message}");
            }
        }
        // Khôi phục
        public async Task<bool> RestoreAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id && a.IsDel);
            if (appointment == null)
                return false;

            appointment.IsDel = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // Xóa
        public async Task<bool> SoftDeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id && !a.IsDel);
            if (appointment == null)
                return false;

            appointment.IsDel = true;
            await _context.SaveChangesAsync();
            return true;
        }
        // Lấy danh sách xóa 
        public async Task<IEnumerable<AppointmentWithUserLawyerDTO>> GetDeletedAppointmentsWithUserLawyerAsync()
        {
            var appointments = await _context.Appointments.Where(a => a.IsDel).ToListAsync();
            var result = new List<AppointmentWithUserLawyerDTO>();
            var userClient = _httpClientFactory.CreateClient("UserService");

            foreach (var appointment in appointments)
            {
                // Lấy thông tin User
                var userLawyerResponse = await userClient.GetAsync($"/api/UserWithLawyerProfile/{appointment.UserId}");
                UserWithLawyerProfileDTO? userWithLawyer = null;
                if (userLawyerResponse.IsSuccessStatusCode)
                {
                    var responseDto = await userLawyerResponse.Content.ReadFromJsonAsync<ResponseDto<UserWithLawyerProfileDTO>>();
                    userWithLawyer = responseDto?.Result;
                }

                // Lấy thông tin Lawyer
                var lawyerProfileResponse = await userClient.GetAsync($"/api/UserWithLawyerProfile/{appointment.LawyerId}");
                UserWithLawyerProfileDTO? lawyerWithProfile = null;
                if (lawyerProfileResponse.IsSuccessStatusCode)
                {
                    var responseDto = await lawyerProfileResponse.Content.ReadFromJsonAsync<ResponseDto<UserWithLawyerProfileDTO>>();
                    lawyerWithProfile = responseDto?.Result;
                }

                result.Add(new AppointmentWithUserLawyerDTO
                {
                    Id = appointment.Id,
                    UserId = appointment.UserId,
                    LawyerId = appointment.LawyerId,
                    ScheduledAt = appointment.ScheduledAt,
                    Slot = appointment.Slot,
                    CreateAt = appointment.CreateAt,
                    Status = appointment.Status,
                    IsDel = appointment.IsDel,
                    Note = appointment.Note,
                    Spec = appointment.Spec,
                    Services = appointment.Services,
                    User = userWithLawyer?.User,
                    LawyerProfile = lawyerWithProfile?.LawyerProfile
                });
            }

            return result;
        }

        //Update 
        public async Task<AppointmentWithUserLawyerDTO?> UpdateAppointmentAsync(int id, Appointment updatedAppointment)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id && !a.IsDel);
            if (appointment == null)
                return null;

            // Cập nhật các trường cho phép sửa
            //appointment.UserId = updatedAppointment.UserId;
            appointment.LawyerId = updatedAppointment.LawyerId;
            appointment.ScheduledAt = updatedAppointment.ScheduledAt;
            appointment.Slot = updatedAppointment.Slot;
            //appointment.CreateAt = updatedAppointment.CreateAt;
            appointment.Status = updatedAppointment.Status;
            appointment.Note = updatedAppointment.Note;
            appointment.Spec = updatedAppointment.Spec;
            appointment.Services = updatedAppointment.Services;

            await _context.SaveChangesAsync();

            // Trả về thông tin chi tiết đã cập nhật
            return await GetAppointmentWithUserLawyerByIdAsync(appointment.Id);
        }


    }
}
