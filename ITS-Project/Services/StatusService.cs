using ITS_Project.Contexts;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITS_Project.Services;



internal class StatusService
{
    private readonly DataContext _context = new();

    public async Task InitializeAsync()
    {
        var statusesVariations = new List<StatusEntity>()
            {
                new StatusEntity { Id = 1, StatusType = "Not started"},
                new StatusEntity { Id = 2, StatusType = "In progress" },
                new StatusEntity { Id = 3, StatusType = "Closed" },
            };

        await _context.AddRangeAsync(statusesVariations);
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