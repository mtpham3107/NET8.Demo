using Microsoft.AspNetCore.Mvc;

namespace NET8.Demo.Admin.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}", Name = "Error")]
    public IActionResult HttpStatusCodeHandler(int statusCode, string ReturnUrl)
    {
        switch (statusCode)
        {
            case 423:
                ViewBag.ErrorMessage = "Tài khoản của bạn đã bị khóa!";
                ViewBag.StatusCode = statusCode;
                ViewBag.ReturnUrl = ReturnUrl;
                return View("Lockout");
            default:
                return RedirectToAction("Index", "Home");
        }
    }
}
