namespace Appointments.Services.API.Models.DTOs
{
    public class AppointmentInfo
    {
        public UserDTO User { get; set; }
        public LawyerProfileDTO? LawyerProfile { get; set; }

        public AppointmentDTO Appointment { get; set; }
    }
}
