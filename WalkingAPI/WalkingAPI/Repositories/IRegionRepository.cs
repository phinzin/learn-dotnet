using WalkingAPI.Models.Domain;

namespace WalkingAPI.Repositories;

public interface IRegionRepository
{
    Task<List<Region?>> GetAllAsync();
    Task<Region?> GetByIdAsync(Guid id);
    Task<Region?> AddAsync(Region? region);
    Task<Region?> UpdateAsync(Guid id, Region region);
    Task<Region?> DeleteAsync(Guid id);
}