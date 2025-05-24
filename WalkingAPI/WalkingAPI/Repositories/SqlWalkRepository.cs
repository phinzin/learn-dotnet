using Microsoft.EntityFrameworkCore;
using WalkingAPI.Models.Domain;

namespace WalkingAPI.Repositories;

public class SqlWalkRepository(WalkDbContext context) : IWalkRepository
{
    public async Task<List<Walk>> GetAllAsync()
    {
        return await context.Walks.Include("Difficulty").Include("Region").ToListAsync();
    }

    public async Task<Walk> GetByIdAsync(Guid id) => await context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=>x.Id == id);

    public async Task<Walk?> AddAsync(Walk? walk)
    {
        await context.Walks.AddAsync(walk);
        await context.SaveChangesAsync();
        return walk;
    }

    public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
    {
        var existingWalk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk == null)
        {
            return null;
        }

        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.DifficultyId = walk.DifficultyId;
        existingWalk.RegionId = walk.RegionId;

        await context.SaveChangesAsync();
        return existingWalk;
    }

    public async Task<Walk?> DeleteAsync(Guid id)
    {
        var existingWalk = await context.Walks.FindAsync(id);
        if (existingWalk == null)
        {
            return null;
        }

        context.Walks.Remove(existingWalk);
        await context.SaveChangesAsync();
        return existingWalk;
    }
}