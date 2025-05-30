namespace capicon_backend.Models.User;

public class UserViewModel {
    public required string Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}