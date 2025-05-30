using Microsoft.AspNetCore.Identity;
using capicon_backend.Models.User;
using Microsoft.EntityFrameworkCore;

namespace capicon_backend.Services;

public class AccountService(
    RoleManager<IdentityRole> roleManager,
    SignInManager<IdentityUser> signInManager,
    UserManager<IdentityUser> userManager)
{
    public async Task<IdentityResult> CreateUserAsync(UserModifyFieldModel userData)
    {
        var user = new IdentityUser
        {
            Email = userData.Email,
            UserName = userData.Username
        };

        var result = await userManager.CreateAsync(user, userData.Password);
        if (!result.Succeeded)
            return result;

        if (await roleManager.RoleExistsAsync(userData.Role))
            await userManager.AddToRoleAsync(user, userData.Role);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> ModifyUserAsync(UserModifyFieldModel model)
    {
        var user = await userManager.FindByIdAsync(model.Id!);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "Пользователь не найден." });

        user.Email = model.Email;
        user.UserName = model.Username;

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return updateResult;

        // TODO: Возможно нужна правильная валидация результата?
        if (!string.IsNullOrWhiteSpace(model.Password))
        {
            var removePasswordResult = await userManager.RemovePasswordAsync(user);
            if (!removePasswordResult.Succeeded)
                return removePasswordResult;

            var addPasswordResult = await userManager.AddPasswordAsync(user, model.Password);
            if (!addPasswordResult.Succeeded)
                return addPasswordResult;
        }

        if (!await roleManager.RoleExistsAsync(model.Role)) return IdentityResult.Success;

        var currentRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, currentRoles);
        await userManager.AddToRoleAsync(user, model.Role);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteUserAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "Пользователь не найден." });
        return await userManager.DeleteAsync(user);
    }

    public async Task<IdentityResult> AuthorizeUserAsync(UserAuthModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "Неверный Email или пароль." });

        var result = await signInManager.PasswordSignInAsync(user.UserName!, model.Password, false, false);

        return result.Succeeded
            ? IdentityResult.Success
            : IdentityResult.Failed(new IdentityError { Description = "Неверный Email или пароль." });
    }

    public async Task<List<UserViewModel>> SearchUsersAsync(string? query, int skip, int take)
    {
        var users = await userManager.Users
            .Where(u => query == null ||
                        u.UserName.Contains(query) ||
                        u.Email.Contains(query))
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        var userViewModels = new List<UserViewModel>();

        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);

            userViewModels.Add(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                Role = roles[0]
            });
        }

        return userViewModels;
    }

    public async Task<UserViewModel?> GetUserByIdAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null) return null;

        var roles = await userManager.GetRolesAsync(user);

        return new UserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.UserName,
            Role = roles[0]
        };
    }

    public async Task<List<string>> GetSystemRolesAsync() => await roleManager.Roles.Select(r => r.Name).ToListAsync();

    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }
}