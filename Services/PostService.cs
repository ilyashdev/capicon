using capicon_backend.Database;
using capicon_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace capicon_backend.Services;

public class PostService(CapiconDBContext context)
{
    public async Task CreatePostAsync(NewsPostModel model)
    {
        context.AddAsync(model);
        await context.SaveChangesAsync();
    }

    public async Task<List<NewsPostModel>> SearchPostsAsync(string? query, int skip, int take) =>
        await context.News.Where(p => p.Title.Contains(query))
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    
    public async Task<NewsPostModel?> GetPost(int id) =>
        await context.News
            .FirstOrDefaultAsync(p => p.Id == id);
    
    public async Task UpdatePost(NewsPostModel product)
    {
        context.News.Update(product);
        await context.SaveChangesAsync();
    }
    public async Task DeletePost(int id)
    {
        var post = await context.News.FindAsync(id);
        if (post != null)
        {
            context.News.Remove(post);
            await context.SaveChangesAsync();
        }
    }
}