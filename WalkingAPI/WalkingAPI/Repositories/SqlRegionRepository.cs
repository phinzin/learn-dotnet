using Microsoft.EntityFrameworkCore;
using WalkingAPI.Models.Domain;

namespace WalkingAPI.Repositories;

public class SqlRegionRepository(WalkDbContext context) : IRegionRepository
{
    public async Task<List<Region?>> GetAllAsync()
    {
        return await context.Regions.ToListAsync();
    }

    public async Task<Region?> GetByIdAsync(Guid id) => await context.Regions.FindAsync(id);

    public async Task<Region?> AddAsync(Region? region)
    {
        await context.Regions.AddAsync(region);
        await context.SaveChangesAsync();
        return region;
    }

    public async Task<Region?> UpdateAsync(Guid id, Region region)
    {
        var existingRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        if (existingRegion == null)
        {
            return null;
        }

        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        existingRegion.RegionImageUrl = region.RegionImageUrl;
        
        await context.SaveChangesAsync();
        return existingRegion;
    }

    public async Task<Region?> DeleteAsync(Guid id)
    {
        var existingRegion = await context.Regions.FindAsync(id);
        if (existingRegion == null)
        {
            return null;
        }

        context.Regions.Remove(existingRegion);
        await context.SaveChangesAsync();
        return existingRegion;
    }
}