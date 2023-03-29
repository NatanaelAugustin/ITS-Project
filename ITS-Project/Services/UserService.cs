using ITS_Project.Contexts;
using ITS_Project.Models;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITS_Project.Services;

internal class UserService
{
    private readonly DataContext _context = new();

    public async Task<UserEntity> CreateAsync(CreateUser createUser)
    {
        var userEntity = new UserEntity
        {
            FirstName = createUser.FirstName,
            LastName = createUser.LastName,
            Email = createUser.Email,
            PhoneNumber = createUser.PhoneNumber,

        };

        await _context.AddAsync(userEntity);
        await _context.SaveChangesAsync();

        return userEntity;
    }

    public async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<UserEntity> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ?? null!;
    }


}
