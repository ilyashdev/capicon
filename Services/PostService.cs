using capicon.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace capicon.Services;

public class PostService
{
    private readonly CSDbContext _context;
    private readonly int DIFF = 8;

    public PostService(CSDbContext context)
    {
        _context = context;
    }

    public async Task AddPostAsync(PostModel model)
    {
        await _context.News.AddAsync(model);
        await _context.SaveChangesAsync();
    }
    public async Task<PostModel?> GetPostAsync(int? id)
    {
        if (id == null) return null;
        return await _context.News.FirstOrDefaultAsync(n => n.Id == id.Value);
    }

    public async Task<List<PostModel>?> GetPostsAsync(int idOffset = 0)
    {
        return await _context.News
            .OrderBy(n => n.dateTime)
            .Skip(idOffset * DIFF)
            .Take(DIFF)
            .ToListAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        var post = await _context.News.FirstAsync(n => n.Id == id);
        _context.News.Remove(post);
        await _context.SaveChangesAsync();
    }

    
}