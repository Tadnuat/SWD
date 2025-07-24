using AutoMapper;
using Users.Services.API.Models.Dtos;
using Users.Services.API.Models;

namespace Users.Services.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
