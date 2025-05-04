namespace capicon.Models;
public class PostModel
{
    public int Id { get; set; }
    public DateTime dateTime { get; set; } = DateTime.UtcNow;
    public string Title { get; set; }
    public string MdText { get; set; }
}