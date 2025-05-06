using capicon.Models;
using DataAccess;

namespace capicon.Services;

public class PostService
{
    private readonly CSDbContext _context;
    private readonly int DIFF = 8;
    
    public PostService(CSDbContext context)
    {
        _context = context;
    }

    public void AddPost(PostModel model)
    {
        _context.News.Add(model);
    }

    public PostModel GetPost(int id)
    {
        return _context.News.First(n => n.Id == id);
    }

    public List<PostModel> GetPosts(int idOffset = 0)
    {
        return _context.News
            .OrderBy(n => n.dateTime)
            .Skip(idOffset * DIFF)
            .Take(DIFF)
            .ToList();
    }
}