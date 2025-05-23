using capicon.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace capicon.Services;

public class PostService(CSDbContext context)
{
    private readonly int DIFF = 8;

    public async Task AddPostAsync(PostModel model)
    {
        await context.News.AddAsync(model);
        await context.SaveChangesAsync();
    }
    public async Task<PostModel?> GetPostAsync(int? id)
    {
        if (id == null) return null;
        return await context.News.FirstOrDefaultAsync(n => n.Id == id.Value);
    }

    public async Task<List<PostModel>?> GetPostsAsync(int idOffset = 0)
    {
        return await context.News
            .OrderBy(n => n.dateTime)
            .Skip(idOffset * DIFF)
            .Take(DIFF)
            .ToListAsync();
    }

    public async Task DeletePostAsync(int id)
    {
        var post = await context.News.FirstAsync(n => n.Id == id);
        context.News.Remove(post);
        await context.SaveChangesAsync();
    }

    
}