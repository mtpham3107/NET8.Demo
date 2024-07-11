using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NET8.Demo.Service1.Controllers;

public class BaseApiController : ControllerBase
{
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public string UserId { get; set; }
    public string Email { get; set; }
    public string Roles { get; set; }
    public string AccessToken { get; set; }

    public BaseApiController(IHttpContextAccessor httpContextAccessor)
    {

        _httpContextAccessor = httpContextAccessor;

        var httpContext = _httpContextAccessor.HttpContext;

        var identity = httpContext?.User.Identity as ClaimsIdentity;

        UserId = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Email = identity?.FindFirst(ClaimTypes.Email)?.Value;
        Roles = identity?.FindFirst(ClaimTypes.Role)?.Value;

        AccessToken = httpContext?.GetTokenAsync("access_token").Result;
    }
}
