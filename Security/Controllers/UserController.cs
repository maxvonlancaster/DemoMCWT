using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;

namespace Security.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static string _userData = "Initial User Data";

    [HttpGet]
    public IActionResult GetUserData()
    {
        return Ok(new { userData = _userData });
    }

    [HttpPost]
    public IActionResult UpdateUserData([FromBody] UserDataUpdateRequest request)
    {
        var sanitizer = new HtmlSanitizer();
        _userData = sanitizer.Sanitize(request.NewUserData);
        return Ok(new { message = "User data updated successfully." });
    }
}

public class UserDataUpdateRequest
{
    public string NewUserData { get; set; }
}