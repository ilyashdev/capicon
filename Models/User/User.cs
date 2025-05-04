using Microsoft.AspNetCore.Identity;

namespace capicon.Models;

public class User : IdentityUser<int> {}

public class UserRole : IdentityRole<int> {}