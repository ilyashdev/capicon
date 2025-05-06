using System.ComponentModel.DataAnnotations;

namespace capicon.Models;
public class PostModel
{
    public int Id { get; set; }
    
    public DateTime dateTime { get; set; } = DateTime.UtcNow;

    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string MdText { get; set; } = default!;
}