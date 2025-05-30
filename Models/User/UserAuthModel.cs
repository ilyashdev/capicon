using System.ComponentModel.DataAnnotations;

namespace capicon_backend.Models.User;

public class UserAuthModel
{
    [Required(ErrorMessage = "Обязательное поле.")]
    public required string Email { get; init; }
    
    [Required(ErrorMessage = "Обязательное поле.")]
    public required string Password { get; init; }
}