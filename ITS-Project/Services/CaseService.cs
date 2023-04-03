using ITS_Project.Contexts;
using ITS_Project.Models;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITS_Project.Services;

internal class CaseService
{
    private readonly DataContext _context = new();

    public async Task<CaseEntity> CreateAsync(CreateCase createCase)
    {
        var caseEntity = new CaseEntity
        {
            UserId = createCase.UserId,
            Subject = createCase.Subject,
            Description = createCase.Description,
        };
        await _context.AddAsync(caseEntity);
        await _context.SaveChangesAsync();

        return caseEntity;
    }

    public async Task<IEnumerable<CaseEntity>> GetAllCasesAsync()
    {
        return await _context.Cases
            .Include(x => x.Comments)
            .Include(x => x.User)
            .Include(x => x.Status)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
    }
    // TODO: hmmm se hur det har blir 
    public async Task<CaseEntity> GetAsync(Guid id)
    {
        return await _context.Cases
            .Include(x => x.Comments)
            .Include(x => x.User)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(x => x.Id == id) ?? null!;

    }

    public async Task<CaseEntity> UpdateCaseAsync(Guid id, int statusId)
    {
        var caseEntity = await GetAsync(id);

        if (!await _context.Statuses.AnyAsync(x => x.Id == statusId))
            return null!;

        caseEntity.StatusId = statusId;

        _context.Update(caseEntity);
        await _context.SaveChangesAsync();

        return caseEntity!;
    }

    public async Task<CaseEntity> DeleteCaseAsync(Guid id)
    {
        var caseEntity = await GetAsync(id);

        _context.Remove(caseEntity);
        await _context.SaveChangesAsync();

        return caseEntity;
    }
}