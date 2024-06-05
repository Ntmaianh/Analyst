using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FPLSP_Analyst.BlazorServer.Controllers;

public class AccountController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Authenticator()
    {
        return Redirect(_configuration["IdentityURLLOGIN"] + "/Account/Login");
    }

    public async Task<IActionResult> Logout()
    {
        // Clear the existing external cookie
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect(_configuration["IdentityURLLOGIN"] + "/Account/Login");
    }
}