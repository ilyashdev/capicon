using System.ComponentModel.DataAnnotations;

namespace capicon_backend.Models.User;

public class UserModifyFieldModel
{
    public string? Id { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле.")]
    public required string Username { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле.")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    
    [Required(ErrorMessage = "Обязательное поле.")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    public required string ConfirmPassword { get; set; }
    
    public required string Role { get; set; }
}