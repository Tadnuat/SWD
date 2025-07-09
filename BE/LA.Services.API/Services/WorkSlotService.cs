using AutoMapper;
using LA.Services.API.Data;
using LA.Services.API.Models;
using LA.Services.API.Models.Dtos;
using LA.Services.API.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace LA.Services.API.Services
{
    public class WorkSlotService : IWorkSlotService
    {
        private readonly LawyerDbContext _db;
        private readonly IMapper _mapper;

        public WorkSlotService(LawyerDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<WorkSlotDto> CreateWorkSlotAsync(int lawyerId, CreateWorkSlotDto createWorkSlotDto)
        {
            WorkSlot workSlot = _mapper.Map<WorkSlot>(createWorkSlotDto);
            workSlot.LawyerId = lawyerId;
            _db.WorkSlots.Add(workSlot);
            await _db.SaveChangesAsync();
            return _mapper.Map<WorkSlotDto>(workSlot);
        }

        public async Task<bool> DeleteWorkSlotAsync(int id)
        {
            try
            {
                WorkSlot workSlot = await _db.WorkSlots.FirstOrDefaultAsync(u => u.Id == id);
                if (workSlot == null)
                {
                    return false;
                }
                workSlot.IsActive = false;
                _db.WorkSlots.Update(workSlot);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<WorkSlotDto>> GetAllWorkSlotsAsync()
        {
            IEnumerable<WorkSlot> workSlots = await _db.WorkSlots.Where(ws => ws.IsActive == true).Include(ws => ws.Lawyer).ToListAsync();
            return _mapper.Map<IEnumerable<WorkSlotDto>>(workSlots);
        }

        public async Task<WorkSlotDto> GetWorkSlotByIdAsync(int id)
        {
            WorkSlot workSlot = await _db.WorkSlots.Where(ws => ws.IsActive == true).Include(ws => ws.Lawyer).FirstOrDefaultAsync(u => u.Id == id);
            return _mapper.Map<WorkSlotDto>(workSlot);
        }

        public async Task<WorkSlotDto> UpdateWorkSlotAsync(int lawyerId, UpdateWorkSlotDto updateWorkSlotDto)
        {
            WorkSlot workSlot = _mapper.Map<WorkSlot>(updateWorkSlotDto);
            workSlot.LawyerId = lawyerId;
            _db.WorkSlots.Update(workSlot);
            await _db.SaveChangesAsync();
            return _mapper.Map<WorkSlotDto>(workSlot);
        }

        public async Task<IEnumerable<WorkSlotDto>> GetWorkSlotsByLawyerIdAsync(int lawyerId)
        {
            IEnumerable<WorkSlot> workSlots = await _db.WorkSlots.Where(ws => ws.LawyerId == lawyerId && ws.IsActive == true).Include(ws => ws.Lawyer).ToListAsync();
            return _mapper.Map<IEnumerable<WorkSlotDto>>(workSlots);
        }

        public async Task<bool> DeactivateWorkSlotAsync(DeactivateWorkSlotDto dto)
        {
            var workSlot = await _db.WorkSlots.FirstOrDefaultAsync(ws => ws.Slot == dto.Slot && ws.DayOfWeek == dto.DayOfWeek && ws.LawyerId == dto.LawyerId && ws.IsActive);
            if (workSlot == null)
            {
                return false;
            }
            workSlot.IsActive = false;
            _db.WorkSlots.Update(workSlot);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivateWorkSlotAsync(ActivateWorkSlotDto dto)
        {
            var workSlot = await _db.WorkSlots.FirstOrDefaultAsync(ws => ws.Slot == dto.Slot && ws.DayOfWeek == dto.DayOfWeek && ws.LawyerId == dto.LawyerId && !ws.IsActive);
            if (workSlot == null)
            {
                return false;
            }
            workSlot.IsActive = true;
            _db.WorkSlots.Update(workSlot);
            await _db.SaveChangesAsync();
            return true;
        }
    }
} 