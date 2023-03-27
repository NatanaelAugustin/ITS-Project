using ITS_Project.Contexts;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITS_Project.Services;

internal class CaseService
{
    private readonly DataContext _context = new();
    private readonly UserService _userService = new();
    private readonly StatusService _statusService = new();

    public async Task CreateAsync(CaseEntity caseEntity)
    {
        if (await _userService.GetAsync(userEntity => userEntity.Id == caseEntity.UserId) != null &&
             await _statusService.GetAsync(statusEntity => statusEntity.Id == caseEntity.StatusId) != null)

        {
            _context.Add(caseEntity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CaseEntity>> GetAllActiveCasesAsync()
    {
        return await _context.Cases
            .Include(x => x.Comments)
            .Include(x => x.User)
            .Include(x => x.Status)
            .Where(x => x.StatusId != 3)
            .OrderByDescending(x => x.Created)
            .ToListAsync();
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

    public async Task<CaseEntity> GetAsync(Expression<Func<CaseEntity, bool>> predicate)
    {

        var _caseEntity = await _context.Cases
            .Include(x => x.Description)
            .Include(x => x.Comments)
            .Include(x => x.User)
            .Include(x => x.Status)
            .FirstOrDefaultAsync(predicate);

        return _caseEntity!;
    }

    public async Task<CaseEntity> UpdateCaseStatusAsync(Expression<Func<CaseEntity, bool>> predicate)
    {
        var _caseEntity = await _context.Cases.FirstOrDefaultAsync(predicate);
        if (_caseEntity != null)
        {
            if (_caseEntity.StatusId == 1)
            {
                _caseEntity.StatusId = 2;

            }
            else if (_caseEntity.StatusId == 2)
            {
                _caseEntity.StatusId = 3;
            }
            else
            {
                _caseEntity.StatusId = 2;
            }


            _context.Update(_caseEntity);
            await _context.SaveChangesAsync();
        }

        return _caseEntity!;
    }
}
