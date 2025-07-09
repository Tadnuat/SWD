using AutoMapper;
using Microsoft.EntityFrameworkCore;
using LA.Services.API.Data;
using LA.Services.API.Models;
using LA.Services.API.Models.Dtos;
using LA.Services.API.Services.IService;

namespace LA.Services.API.Services
{
    public class LawyerDiplomaService : ILawyerDiplomaService
    {
        private readonly LawyerDbContext _context;
        private readonly IMapper _mapper;

        public LawyerDiplomaService(LawyerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<LawyerDiplomaDTO>>> GetAllDiplomasAsync(bool includeDeleted = false)
        {
            var response = new ResponseDto<IEnumerable<LawyerDiplomaDTO>>();
            try
            {
                var query = _context.Diplomas.AsQueryable();
                if (!includeDeleted)
                {
                    query = query.Where(d => !d.IsDeleted);
                }
                var diplomas = await query.ToListAsync();
                response.Result = _mapper.Map<IEnumerable<LawyerDiplomaDTO>>(diplomas);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<LawyerDiplomaDTO>> GetDiplomaByIdAsync(int id)
        {
            var response = new ResponseDto<LawyerDiplomaDTO>();
            try
            {
                var diploma = await _context.Diplomas.FindAsync(id);
                if (diploma == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No diploma found with id = {id}";
                    return response;
                }
                response.Result = _mapper.Map<LawyerDiplomaDTO>(diploma);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<IEnumerable<LawyerDiplomaDTO>>> GetDiplomasByLawyerIdAsync(int lawyerId, bool includeDeleted = false)
        {
            var response = new ResponseDto<IEnumerable<LawyerDiplomaDTO>>();
            try
            {
                var query = _context.Diplomas.Where(d => d.LawyerId == lawyerId);
                if (!includeDeleted)
                {
                    query = query.Where(d => !d.IsDeleted);
                }
                var diplomas = await query.ToListAsync();
                response.Result = _mapper.Map<IEnumerable<LawyerDiplomaDTO>>(diplomas);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<LawyerDiplomaDTO>> CreateDiplomaAsync(LawyerDiplomaDTO diplomaDto)
        {
            var response = new ResponseDto<LawyerDiplomaDTO>();
            try
            {
                var diploma = _mapper.Map<LawyerDiploma>(diplomaDto);
                diploma.CreatedAt = DateTime.Now;
                diploma.UpdatedAt = DateTime.Now;
                diploma.IsDeleted = false;

                await _context.Diplomas.AddAsync(diploma);
                await _context.SaveChangesAsync();

                response.Result = _mapper.Map<LawyerDiplomaDTO>(diploma);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<LawyerDiplomaDTO>> UpdateDiplomaAsync(int id, LawyerDiplomaDTO diplomaDto)
        {
            var response = new ResponseDto<LawyerDiplomaDTO>();
            try
            {
                var existingDiploma = await _context.Diplomas.FindAsync(id);
                if (existingDiploma == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No diploma found with id = {id}";
                    return response;
                }

                _mapper.Map(diplomaDto, existingDiploma);
                existingDiploma.UpdatedAt = DateTime.Now;

                _context.Diplomas.Update(existingDiploma);
                await _context.SaveChangesAsync();

                response.Result = _mapper.Map<LawyerDiplomaDTO>(existingDiploma);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<bool>> DeleteDiplomaAsync(int id)
        {
            var response = new ResponseDto<bool>();
            try
            {
                var diploma = await _context.Diplomas.FindAsync(id);
                if (diploma == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No diploma found with id = {id}";
                    return response;
                }

                diploma.IsDeleted = true;
                diploma.UpdatedAt = DateTime.Now;

                _context.Diplomas.Update(diploma);
                await _context.SaveChangesAsync();

                response.Result = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
} 