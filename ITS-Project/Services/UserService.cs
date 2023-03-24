using ITS_Project.Contexts;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITS_Project.Services;

internal class UserService
{
    private readonly DataContext _context = new();

    public async Task CreateAsync(UserEntity entity)
    {
        var _entity = await _context.Users.FirstOrDefaultAsync(x => x.Email == entity.Email);
        if (_entity != null)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<UserEntity> GetAsync(Func<UserEntity, bool> predicate)
    {
        var _entity = await _context.Users.FindAsync(predicate);
        return _entity!;
    }
}
