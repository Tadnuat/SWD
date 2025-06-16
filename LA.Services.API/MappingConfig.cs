using AutoMapper;
using LA.Services.API.Models;
using LA.Services.API.Models.Dtos;

namespace LA.Services.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<LawyerProfile, LawyerProfileDTO>().ReverseMap();
            CreateMap<LawyerDiploma, LawyerDiplomaDTO>().ReverseMap();
            CreateMap<WorkSlot, WorkSlotDto>().ReverseMap();
            CreateMap<WorkSlot, CreateWorkSlotDto>().ReverseMap();
            CreateMap<WorkSlot, UpdateWorkSlotDto>().ReverseMap();
        }
    }
}
