using ITS_Project.Contexts;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITS_Project.Services;

internal class StatusService
{
    private readonly DataContext _context = new();

    public async Task CreateStatusTypesAsync()
    {
        if (!await _context.Statuses.AnyAsync())
        {
            string[] _statuses = new string[] { "Ej på börjad", "Påbörjad", "Avslutad" };

            foreach (var status in _statuses)
            {
                await _context.AddAsync(new StatusEntity { StatusName = status });
                await _context.SaveChangesAsync();
            }

        }
    }
}
