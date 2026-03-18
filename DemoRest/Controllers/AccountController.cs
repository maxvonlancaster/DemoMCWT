using DemoRest.Models;
using DemoRest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoRest.Controllers;
[Route("[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private static readonly List<User> users = new List<User>
    {
        new User { Id = 1, Email = "user1", PasswordHash = "password1" },
        new User { Id = 2, Email = "user2", PasswordHash = "password2" }
    };

    public AccountController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = users
            .FirstOrDefault(u => u.Email == request.Email && u.PasswordHash == request.Password);
        if (user == null)
        {
            return Unauthorized("Invalid email or password.");
        }
        var token = _tokenService.GenerateToken(user.Email);
        return Ok(new { Token = token });
    }

    //[HttpGet("current-user")]
    //public IActionResult GetCurrentUser()
    //{
    //    var email = User.Identity?.Name;
    //    if (email == null)
    //    {
    //        return Unauthorized("User is not authenticated.");
    //    }
    //    var user = users.FirstOrDefault(u => u.Email == email);
    //    if (user == null)
    //    {
    //        return NotFound("User not found.");
    //    }
    //    return Ok(new { user.Id, user.Email });
    //}

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        var email = User.Identity?.Name;
        if (email == null)
        {
            return Unauthorized("User is not authenticated.");
        }
        var user = users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        user.IsLoggedIn = false;
        return Ok("Logged out successfully.");
    }
}
