using System.ComponentModel.DataAnnotations;

namespace capicon.Models;
public class UserShowModel
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public IList<string> Roles { get; set; } = default!;
}
