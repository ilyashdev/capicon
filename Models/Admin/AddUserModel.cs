using System.ComponentModel.DataAnnotations;

public class AddUserModel
{
    [Required(ErrorMessage = "Обязательное поле.")]
    [Display(Name = "Логин пользователя")]
    public string UserLogin { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    [Display(Name = "Роль")]
    public string UserRole { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
    [Display(Name = "Подтвердите пароль")]
    public string ConfirmPassword { get; set; } = default!;
}
