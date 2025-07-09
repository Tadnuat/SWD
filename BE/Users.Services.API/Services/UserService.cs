using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Users.Services.API.Data;
using Users.Services.API.Models;
using Users.Services.API.Models.Dtos;
using Users.Services.API.Services.IService;

namespace Users.Services.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public UserService(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<UserDTO>>> GetAllUsersAsync(bool includeInactive = false)
        {
            var response = new ResponseDto<IEnumerable<UserDTO>>();
            try
            {
                var query = _context.Users.AsQueryable();
                if (!includeInactive)
                {
                    query = query.Where(u => u.IsActive);
                }
                var users = await query.ToListAsync();
                response.Result = _mapper.Map<IEnumerable<UserDTO>>(users);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<UserDTO>> GetUserByIdAsync(int id)
        {
            var response = new ResponseDto<UserDTO>();
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No user found with id = {id}";
                    return response;
                }
                response.Result = _mapper.Map<UserDTO>(user);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<UserDTO>> CreateUserAsync(UserDTO userDto)
        {
            var response = new ResponseDto<UserDTO>();
            try
            {
                var userEntity = _mapper.Map<User>(userDto);
                userEntity.IsActive = true;
                _context.Users.Add(userEntity);
                await _context.SaveChangesAsync();
                response.Result = _mapper.Map<UserDTO>(userEntity);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto<bool>> UpdateUserAsync(int id, UserDTO userDto)
        {
            var response = new ResponseDto<bool>();
            try
            {
                var existing = await _context.Users.FindAsync(id);
                if (existing == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No user found with id = {id}";
                    response.Result = false;
                    return response;
                }
                _mapper.Map(userDto, existing);
                _context.Users.Update(existing);
                await _context.SaveChangesAsync();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.Result = false;
            }
            return response;
        }

        public async Task<ResponseDto<bool>> SoftDeleteUserAsync(int id)
        {
            var response = new ResponseDto<bool>();
            try
            {
                var existing = await _context.Users.FindAsync(id);
                if (existing == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No user found with id = {id}";
                    response.Result = false;
                    return response;
                }
                existing.IsActive = false;
                _context.Users.Update(existing);
                await _context.SaveChangesAsync();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.Result = false;
            }
            return response;
        }

        public async Task<ResponseDto<bool>> HardDeleteUserAsync(int id)
        {
            var response = new ResponseDto<bool>();
            try
            {
                var existing = await _context.Users.FindAsync(id);
                if (existing == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No user found with id = {id}";
                    response.Result = false;
                    return response;
                }
                _context.Users.Remove(existing);
                await _context.SaveChangesAsync();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.Result = false;
            }
            return response;
        }

        public async Task<ResponseDto<bool>> RestoreUserAsync(int id)
        {
            var response = new ResponseDto<bool>();
            try
            {
                var existing = await _context.Users.FindAsync(id);
                if (existing == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"No user found with id = {id}";
                    response.Result = false;
                    return response;
                }
                existing.IsActive = true;
                _context.Users.Update(existing);
                await _context.SaveChangesAsync();
                response.Result = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.Result = false;
            }
            return response;
        }
    }
}