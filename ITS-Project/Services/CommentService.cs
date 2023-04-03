using ITS_Project.Contexts;
using ITS_Project.Models;
using ITS_Project.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace ITS_Project.Services;

internal class CommentService
{
    private readonly DataContext _context = new();

    public async Task<CommentEntity> CreateAsync(CreateComment createComment)
    {
        var commentEntity = new CommentEntity
        {
            CaseId = createComment.CaseId,
            CommentText = createComment.CommentText
        };

        await _context.AddAsync(commentEntity);
        await _context.SaveChangesAsync();

        return commentEntity;
    }

    public async Task<IEnumerable<CommentEntity>> GetByCaseId(Guid caseId)
    {
        return await _context.Comments.Where(x => x.CaseId == caseId).OrderBy(x => x.Created).ToListAsync();
    }
    public async Task<IEnumerable<CommentEntity>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }
}







