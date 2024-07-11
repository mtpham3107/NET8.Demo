using Microsoft.AspNetCore.Identity;

namespace NET8.Demo.Admin.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Avatar { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public string FullName => $"{LastName?.Trim()} {FirstName?.Trim()}".Trim();
}
