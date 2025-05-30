using System.ComponentModel.DataAnnotations;

namespace capicon_backend.Models;

public class NewsPostModel
{
    public required int Id { get; init; }
    
    public required DateTime dateTime { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Укажите заголовок.")]
    public required string Title { get; set; }

    public string? MdText { get; set; }
}