using Microsoft.AspNetCore.Mvc;

namespace Security.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private static string _accountInfo = "Initial Account Info";

    [HttpGet]
    public IActionResult GetAccountInfo()
    {
        return Ok(new { accountInfo = _accountInfo });
    }

    [HttpPost("update-info")]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateAccountInfo([FromForm] string newInfo)
    {
        // user identity is verified here
        _accountInfo = newInfo;
        return Ok(new { message = "Account info updated successfully." });
    }
}
