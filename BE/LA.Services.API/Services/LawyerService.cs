using AutoMapper;
using LA.Services.API.Data;
using LA.Services.API.Models;
using LA.Services.API.Models.Dtos;
using LA.Services.API.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace LA.Services.API.Services
{
    public class LawyerService : ILawyerService
    {
        private readonly LawyerDbContext _context;
        private readonly IMapper _mapper;

        public LawyerService(LawyerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LawyerProfileDTO>> GetAllLawyersAsync()
        {
            var lawyers = await _context.LawyerProfiles.ToListAsync();
            return _mapper.Map<IEnumerable<LawyerProfileDTO>>(lawyers);
        }

        public async Task<LawyerProfileDTO?> GetLawyerByIdAsync(int id)
        {
            var lawyer = await _context.LawyerProfiles.FindAsync(id);
            return lawyer == null ? null : _mapper.Map<LawyerProfileDTO>(lawyer);
        }

        public async Task<LawyerProfileDTO> CreateLawyerAsync(LawyerProfileDTO profileDto)
        {
            var lawyerEntity = _mapper.Map<LawyerProfile>(profileDto);

            
            _context.LawyerProfiles.Add(lawyerEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<LawyerProfileDTO>(lawyerEntity);
        }

        public async Task<bool> UpdateLawyerAsync(int id, LawyerProfileDTO profileDto)
        {
            var existing = await _context.LawyerProfiles.FindAsync(id);
            if (existing == null) return false;

            
            _mapper.Map(profileDto, existing);

           
            _context.LawyerProfiles.Update(existing);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LawyerProfileDTO?> GetLawyerByUserIdAsync(int userId)
        {
            var lawyer = await _context.LawyerProfiles.FirstOrDefaultAsync(l => l.UserId == userId);
            return lawyer == null ? null : _mapper.Map<LawyerProfileDTO>(lawyer);
        }

        public async Task<bool> DeleteLawyerAsync(int id)
        {
            var existing = await _context.LawyerProfiles.FindAsync(id);
            if (existing == null) return false;

            _context.LawyerProfiles.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<LawyerProfileDTO?> UpdateLawyerProfileAsync(int id, UpdateLawyerDTO dto)
        {
            var lawyer = await _context.LawyerProfiles.FindAsync(id);
            if (lawyer == null) return null;

            // Cập nhật các trường từ DTO
            lawyer.Bio = dto.Bio;
            lawyer.Spec = dto.Spec ?? new List<string>();
            lawyer.LicenseNum = dto.LicenseNum;
            lawyer.ExpYears = dto.ExpYears;
            lawyer.Description = dto.Description;
            lawyer.Rating = dto.Rating;
            lawyer.PricePerHour = dto.PricePerHour;
            lawyer.Img = dto.Img;
            lawyer.DayOfWeek = dto.DayOfWeek;
            lawyer.WorkTime = dto.WorkTime;

            await _context.SaveChangesAsync();

            return _mapper.Map<LawyerProfileDTO>(lawyer);
        }
    }
}
