using System;
using Users.Services.API.Models.Dtos;

namespace Users.Services.API.Services.IService
{
    public interface IUserService
    {
        Task<ResponseDto<IEnumerable<UserDTO>>> GetAllUsersAsync(bool includeInactive = false);
        Task<ResponseDto<UserDTO>> GetUserByIdAsync(int id);
        Task<ResponseDto<UserDTO>> CreateUserAsync(UserDTO userDto);
        Task<ResponseDto<bool>> UpdateUserAsync(int id, UserDTO userDto);
        Task<ResponseDto<bool>> SoftDeleteUserAsync(int id);
        Task<ResponseDto<bool>> HardDeleteUserAsync(int id);
        Task<ResponseDto<bool>> RestoreUserAsync(int id);
    }
}
