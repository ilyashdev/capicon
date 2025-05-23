using capicon.Models;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using capicon.Settings;
public class PostService
{
    private readonly CSDbContext _context;

    public PostService(CSDbContext context)
    {
        _context = context;
    }

    public async Task CreatePost(PostModel? product)
    {
        _context.News.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePost(PostModel? product)
    {
        _context.News.Remove(product);
        await _context.SaveChangesAsync();
    }
    public async Task<List<PostModel>> SearchPosts(string query, int page) =>
        await _context.News
            .OrderBy(n => n.dateTime)
            .Where(p => p.Title.Contains(query))
            .Skip(page * PageSettings.PAGE_SIZE)
            .Take(PageSettings.PAGE_SIZE)
            .ToListAsync();

    public async Task<PostModel?> GetPost(int id) =>
        await _context.News
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task UpdatePost(PostModel? product)
    {
        _context.News.Update(product);
        await _context.SaveChangesAsync();
    }
}