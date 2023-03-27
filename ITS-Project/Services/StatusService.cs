using ITS_Project.Contexts;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITS_Project.Services;

internal class StatusService
{
    private readonly DataContext _context = new();

    public async Task InitAsync()
    {
        if (!await _context.Statuses.AnyAsync())
        {
            var statusStates = new List<StatusEntity>();
            {
                new StatusEntity() { StatusType = "Ej påbörjad" };

                new StatusEntity() { StatusType = "Avklarad" };

                new StatusEntity() { StatusType = "Pågående" };

            };

            _context.AddRange(statusStates);

            await _context.SaveChangesAsync();

        }
    }
    public async Task<IEnumerable<StatusEntity>> GetAllAsync()
    {
        return await _context.Statuses.ToListAsync();
    }

    public async Task<StatusEntity> GetAsync(Expression<Func<StatusEntity, bool>> predicate)
    {
        var _statusEntity = await _context.Statuses.FirstOrDefaultAsync(predicate);
        return _statusEntity!;

    }
}
