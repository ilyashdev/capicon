using capicon.Models;
using DataAccess;

namespace capicon.Services;

public class PostsService
{
    private readonly CSDbContext _context;
    private readonly int DIFF = 8;
    
    public PostsService(CSDbContext context)
    {
        _context = context;
    }

    public PostModel GetPost(int id)
    {
        return _context.News.First(n => n.Id == id);
    }

    public List<PostModel> GetPostsByOffset(int id)
    {
        return _context.News
            .OrderBy(n => n.dateTime)
            .Skip(id * DIFF)
            .Take(DIFF)
            .ToList();
    }
}