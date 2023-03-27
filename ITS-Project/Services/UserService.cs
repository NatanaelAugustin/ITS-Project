using ITS_Project.Contexts;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITS_Project.Services;

internal class UserService
{
    private readonly DataContext _context = new();

    public async Task<UserEntity> CreateAsync(UserEntity userEntity)
    {
        var _userEntity = await GetAsync(x => x.Email == userEntity.Email);
        if (_userEntity == null)
        {
            _userEntity = userEntity;
            _context.Add(_userEntity);
            await _context.SaveChangesAsync();
        }

        return _userEntity;
    }

    public async Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        var _userEntity = await _context.Users.FirstOrDefaultAsync(predicate);
        return _userEntity!;
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

}
