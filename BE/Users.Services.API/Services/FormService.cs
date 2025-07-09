using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Services.API.Data;
using Users.Services.API.Models;
using Users.Services.API.Models.Dtos;
using Users.Services.API.Services.IService;

namespace Users.Services.API.Services
{
    public class FormService : IFormService
    {
        private readonly UserDbContext _context;
        public FormService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Form>> GetAllAsync()
        {
            return await _context.Forms.ToListAsync();
        }

        public async Task<Form?> GetByIdAsync(int id)
        {
            return await _context.Forms.FindAsync(id);
        }

        public async Task<Form> CreateAsync(FormDTO dto)
        {
            var form = new Form
            {
                Name = dto.Name,
                Description = dto.Description,
                FilePath = dto.FilePath
            };
            _context.Forms.Add(form);
            await _context.SaveChangesAsync();
            return form;
        }

        public async Task<Form?> UpdateAsync(int id, FormDTO dto)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form == null) return null;
            form.Name = dto.Name;
            form.Description = dto.Description;
            form.FilePath = dto.FilePath;
            await _context.SaveChangesAsync();
            return form;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var form = await _context.Forms.FindAsync(id);
            if (form == null) return false;
            _context.Forms.Remove(form);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 