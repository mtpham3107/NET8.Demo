using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NET8.Demo.Admin.Entities;
using System.Security.Claims;

namespace NET8.Demo.Admin.Application.Services;

public class ProfileService: UserClaimsPrincipalFactory<User, IdentityRole>, IProfileService
{
    private readonly UserManager<User> _userManager;
    public ProfileService(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);

        var identity = await base.GenerateClaimsAsync(user);

        // Add custom claims
        identity.AddClaim(new Claim("userId", user.Id));
        identity.AddClaim(new Claim("fullName", user.FullName ?? ""));
        identity.AddClaim(new Claim("avatar", user.Avatar ?? ""));
        identity.AddClaim(new Claim("email", user.Email ?? ""));

        context.IssuedClaims.AddRange(identity.Claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await _userManager.GetUserAsync(context.Subject);

        context.IsActive = (user != null);
    }
}