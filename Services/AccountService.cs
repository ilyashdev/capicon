using capicon.Models;
using Microsoft.AspNetCore.Identity;

public interface IAccountService
{
    Task<(bool Success, IEnumerable<string> Errors)> RegisterUserAsync(AddUserModel model);
    Task<(bool Success, IEnumerable<string> Errors)> UpdateUserAsync(ModifyUserModel model);
    Task<(bool Success, IEnumerable<string> Errors)> DeleteUserAsync(string userId);
    Task<IList<string>> GetRolesAsync(User user);
    Task<List<string>> GetAllRolesAsync();
    Task<User?> GetUserByIdAsync(string userId);
    IEnumerable<User> GetAllUsers();
}

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<UserRole> _roleManager;
    private readonly ILogger<AccountService> _logger;

    public AccountService(UserManager<User> userManager,
                          RoleManager<UserRole> roleManager,
                          ILogger<AccountService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<(bool Success, IEnumerable<string> Errors)> RegisterUserAsync(AddUserModel model)
    {
        if (!await _roleManager.RoleExistsAsync(model.UserRole))
        {
            return (false, new[] { "Указанная роль не существует." });
        }

        var existingUser = await _userManager.FindByNameAsync(model.UserLogin);
        if (existingUser != null)
        {
            return (false, new[] { "Пользователь с таким логином уже существует." });
        }

        var user = new User
        {
            UserName = model.UserLogin,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return (false, result.Errors.Select(e => e.Description));
        }

        await _userManager.AddToRoleAsync(user, model.UserRole);
        _logger.LogInformation("User registered: {UserLogin}", model.UserLogin);

        return (true, Enumerable.Empty<string>());
    }

    public async Task<(bool Success, IEnumerable<string> Errors)> UpdateUserAsync(ModifyUserModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserLogin);
        if (user == null)
            return (false, new[] { "Пользователь не найден." });

        user.Email = model.Email;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return (false, updateResult.Errors.Select(e => e.Description));

        var currentRoles = await _userManager.GetRolesAsync(user);
        if (!await _roleManager.RoleExistsAsync(model.UserRole))
            return (false, new[] { "Указанная роль не существует." });

        var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeRoleResult.Succeeded)
            return (false, removeRoleResult.Errors.Select(e => e.Description));

        var addRoleResult = await _userManager.AddToRoleAsync(user, model.UserRole);
        if (!addRoleResult.Succeeded)
            return (false, addRoleResult.Errors.Select(e => e.Description));

        if (!string.IsNullOrWhiteSpace(model.Password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (!passwordResult.Succeeded)
                return (false, passwordResult.Errors.Select(e => e.Description));
        }

        _logger.LogInformation("User updated: {UserLogin}", model.UserLogin);
        return (true, Enumerable.Empty<string>());
    }

    public async Task<(bool Success, IEnumerable<string> Errors)> DeleteUserAsync(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return (false, new[] { "Неверный ID пользователя." });
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return (false, new[] { "Пользователь не найден." });

        var userName = user.UserName;

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            return (false, result.Errors.Select(e => e.Description));

        _logger.LogInformation("Пользователь {userName} ({userId}) удалён.", userName, userId);
        return (true, Enumerable.Empty<string>());
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userManager.Users.ToList();
    }

    public Task<List<string>> GetAllRolesAsync()
    {
        var roles = _roleManager.Roles.ToList();
        return Task.FromResult<List<string>>([.. roles.Select(role => role.Name)]);
    }



    public async Task<User?> GetUserByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<IList<string>> GetRolesAsync(User user)
    {
        if (user == null)
        {
            return new List<string>();
        }

        return await _userManager.GetRolesAsync(user);
    }
}
