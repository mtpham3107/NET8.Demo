using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NET8.Demo.Admin.Entities;
using NET8.Demo.Admin.Models;

namespace NET8.Demo.Admin.Controllers;

public class AccountController : BaseController
{
    private SignInManager<User> _signInManager;
    private UserManager<User> _userManager;
    private readonly IIdentityServerInteractionService _interaction;

    public AccountController(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IStringLocalizer<AccountController> localizer,
        IIdentityServerInteractionService interaction) : base(localizer)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _interaction = interaction;
    }

    #region Login - Logoff
    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

        if (result.Succeeded)
        {
            return RedirectToLocal(returnUrl);
        }

        if (result.IsLockedOut)
        {
            return RedirectToRoute("Error", new { statusCode = 423, ReturnUrl = returnUrl });
        }
        else
        {
            ModelState.AddModelError("", Localized("InvalidLoginAttempt"));
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        var logoutContext = await _interaction.GetLogoutContextAsync(logoutId);

        await _signInManager.SignOutAsync();

        if (!string.IsNullOrWhiteSpace(logoutContext.PostLogoutRedirectUri))
        {
            return Redirect(logoutContext.PostLogoutRedirectUri);
        }

        return NoContent();
    }

    #endregion

    #region Register 
    [AllowAnonymous]
    public ActionResult Register(string returnUrl)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                LockoutEnabled = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                user = await _userManager.FindByEmailAsync(user.Email);

                //await _userManager.AddToRoleAsync(user, "User");

                await _signInManager.SignInAsync(user, isPersistent: false);

                // Send an email with this link
                //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme); 
                //SendMail("Xác nhận tài khoản", "Vui lòng xác nhận tài khoản bằng cách nhấn vào link <a href=\"" + callbackUrl + "\"> này</a>", model.Email);

                return RedirectToLocal(returnUrl);
            }
            AddErrors(result);
        }
        ViewBag.ReturnUrl = returnUrl;
        return View(model);
    }
    #endregion

    private ActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction("Index", "Home");
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
    }
}