using System.ComponentModel.DataAnnotations;

namespace capicon.Models;
public class UserViewModel
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public IList<string> Roles { get; set; } = default!;
}

public class CreateUserModel
{
    [Required(ErrorMessage = "Обязательное поле.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "Обязательное поле.")]
    public string Role { get; set; } = default!;
}

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
