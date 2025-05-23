using capicon.Models;
using capicon.Settings;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace capicon.Services;

public class PostService(CSDbContext context)
{
    public async Task CreatePost(PostModel? product)
    {
        context.News.Add(product);
        await context.SaveChangesAsync();
    }

    public async Task<List<PostModel>> SearchPosts(string? query, int page)
        => await context.News
            .Where(p => p.Title.Contains(query))
            .Skip(page * PageSettings.PAGE_SIZE)
            .Take(PageSettings.PAGE_SIZE)
            .ToListAsync();

    public async Task<int> getPageCount()
    {
        var pages = await context.News.CountAsync();
        return pages / PageSettings.PAGE_SIZE;
    }

    public async Task<PostModel?> GetPost(int id) =>
        await context.News
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task UpdatePost(PostModel? product)
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