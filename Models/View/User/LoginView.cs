using System.ComponentModel.DataAnnotations;

namespace capicon.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Обязательное поле.")]
    [EmailAddress(ErrorMessage = "Некорректный e-mail.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = default!;

    [Display(Name = "Запомнить меня")]
    public bool RememberMe { get; set; }
}

