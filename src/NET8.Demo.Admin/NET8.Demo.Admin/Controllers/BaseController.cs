using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace NET8.Demo.Admin.Controllers;

public class BaseController : Controller
{
    protected readonly IStringLocalizer<BaseController> _localizer;

    public BaseController(IStringLocalizer<BaseController> localizer)
    {
        _localizer = localizer;
    }

    public string Localized(string key)
    {
        return _localizer[key];
    }
}
