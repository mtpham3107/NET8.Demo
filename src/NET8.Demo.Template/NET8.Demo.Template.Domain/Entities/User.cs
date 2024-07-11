using Microsoft.AspNetCore.Identity;

namespace NET8.Demo.Template.Entities;

public class User : IdentityUser
{
    public string FristName { get; set; }

    public string LastName { get; set; }

    public string Avatar { get; set; }
}
