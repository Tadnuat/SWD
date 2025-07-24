using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LA.Services.API.Models;
using LA.Services.API.Data;

namespace LA.Services.API.Repository
{
    public class WorkSlotRepository : IWorkSlotRepository
    {
        private readonly LawyerDbContext _context;
        public WorkSlotRepository(LawyerDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<WorkSlot>> GetAllAsync() => await _context.WorkSlots.ToListAsync();
        public async Task<WorkSlot?> GetByIdAsync(int id) => await _context.WorkSlots.FindAsync(id);
        public async Task AddAsync(WorkSlot workSlot)
        {
            _context.WorkSlots.Add(workSlot);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(WorkSlot workSlot)
        {
            _context.WorkSlots.Update(workSlot);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var ws = await _context.WorkSlots.FindAsync(id);
            if (ws != null)
            {
                _context.WorkSlots.Remove(ws);
                await _context.SaveChangesAsync();
            }
        }
    }
} 