using ITS_Project.Contexts;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITS_Project.Services;

internal class StatusService
{
    private readonly DataContext _context = new();

    public async Task StartAsync()
    {
        var statuses = new List<StatusEntity>()
            {
                new StatusEntity { Id = 1, StatusType = "Pending"},
                new StatusEntity { Id = 2, StatusType = "Ongoing" },
                new StatusEntity { Id = 3, StatusType = "Finished" },
            };

        await _context.AddRangeAsync(statuses);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<StatusEntity>> GetAllAsync()
    {
        return await _context.Statuses.ToListAsync();
    }

    public async Task<StatusEntity> GetAsync(int id)
    {
        return await _context.Statuses.FirstOrDefaultAsync(x => x.Id == id) ?? null!;
    }
}