using AutoMapper;
using WalkingAPI.Models.Domain;
using WalkingAPI.Models.DTO;

namespace WalkingAPI.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
    }
}