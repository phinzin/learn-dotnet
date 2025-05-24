using AutoMapper;
using WalkingAPI.Models.Domain;
using WalkingAPI.Models.DTO;

namespace WalkingAPI.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<AddWalkRequestDto, Walk>();
        CreateMap<UpdateWalkRequestDto, Walk>();
        CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        CreateMap<AddDifficultyRequestDto, Difficulty>();
        CreateMap<UpdateDifficultyRequestDto, Difficulty>();
    }
}