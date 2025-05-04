using capicon.Models;

namespace capicon.Services;

public class AccountService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountService(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<List<UserViewModel>> GetAllUsersAsync()
    {
        var users = _userManager.Users.ToList();
        var result = new List<UserViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            result.Add(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!,
                Roles = roles
            });
        }

        return result;
    }

    public List<string> GetAllRoles()
    {
        return _roleManager.Roles.Select(r => r.Name).ToList()!;
    }

    public async Task<UserViewModel?> GetUserByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return null;

        var roles = await _userManager.GetRolesAsync(user);

        return new UserViewModel
        {
            Id = user.Id,
            Email = user.Email!,
            UserName = user.UserName!,
            Roles = roles
        };
    }

    public async Task<IdentityResult> AddUserAsync(CreateUserModel model)
    {
        var user = new IdentityUser
        {
            Email = model.Email,
            UserName = model.UserName
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return result;

        if (await _roleManager.RoleExistsAsync(model.Role))
            await _userManager.AddToRoleAsync(user, model.Role);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> RemoveUserAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        return await _userManager.DeleteAsync(user);
    }

    public async Task<IdentityResult> ModifyUserAsync(ModifyUserModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        user.Email = model.Email;
        user.UserName = model.UserName;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            return updateResult;

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);

        if (await _roleManager.RoleExistsAsync(model.Role))
            await _userManager.AddToRoleAsync(user, model.Role);

        return IdentityResult.Success;
    }
}
