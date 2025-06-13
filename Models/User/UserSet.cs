using System.ComponentModel.DataAnnotations;

namespace capicon.Models;
public class UserSetFieldModel
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "Обязательное поле.")]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    public string Role { get; set; } = default!;
}
