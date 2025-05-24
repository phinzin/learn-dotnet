using WalkingAPI.Models.Domain;

namespace WalkingAPI.Repositories;

public interface IWalkRepository
{
    Task<List<Walk?>> GetAllAsync();
    Task<Walk> GetByIdAsync(Guid id);
    Task<Walk?> AddAsync(Walk? walk);
    Task<Walk?> UpdateAsync(Guid id, Walk walk);
    Task<Walk?> DeleteAsync(Guid id);
}