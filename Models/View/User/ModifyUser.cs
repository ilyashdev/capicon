using System.ComponentModel.DataAnnotations;

namespace capicon.Models;

public class ModifyUserModel
{
    public string Id { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    public string Role { get; set; } = default!;
}
