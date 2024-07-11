using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Logging;
using NET8.Demo.Admin.Entities;

namespace NET8.Demo.Admin.Application.Services;
public class UserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<UserService> _logger;

    public UserService(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<UserService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }
}
