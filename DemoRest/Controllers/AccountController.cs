using DemoRest.Models;
using DemoRest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoRest.Controllers;
[Route("[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public AccountController(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _userService.GetUser(request.Email, request.Password);
        if (user == null)
        {
            return Unauthorized("Invalid email or password.");
        }
        var token = _tokenService.GenerateToken(user.Email);
        return Ok(new { Token = token });
    }

    [HttpGet("current-user")]
    public IActionResult GetCurrentUser()
    {
        var email = User.Identity?.Name;
        if (email == null)
        {
            return Unauthorized("User is not authenticated.");
        }
        var user = _userService.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return Ok(new { user.Id, user.Email });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        var email = User.Identity?.Name;
        if (email == null)
        {
            return Unauthorized("User is not authenticated.");
        }
        var user = _userService.GetUserByEmail(email);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        user.IsLoggedIn = false;
        return Ok("Logged out successfully.");
    }
}


// Single Responsibility:
public class UserService : IUserService
{
    private static readonly List<User> users = new List<User>
    {
        new User { Id = 1, Email = "user1", PasswordHash = "password1" },
        new User { Id = 2, Email = "user2", PasswordHash = "password2" }
    };

    public User? GetUser(string email, string password)
    {
        return users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
    }

    public User? GetUserByEmail(string email) => users.FirstOrDefault(u => u.Email == email);
}

// Advantages of SRP:
// Maintanability
// Testability
// Reusability



// Open/Closed Principle:
// Service has to be open for extension but closed for modification.

// Bad example:
//public class DiscountService
//{
//    public decimal CalculateDiscount(decimal totalAmount, string customerType)
//    {
//        if (customerType == "Regular")
//        {
//            return totalAmount * 0.1m; // 10% discount for regular customers
//        }
//        else if (customerType == "VIP")
//        {
//            return totalAmount * 0.2m; // 20% discount for VIP customers
//        }
//        return 0; // No discount for other customer types
//    }
//}

// Good example:
public interface IDiscountStrategy
{
    decimal CalculateDiscount(decimal totalAmount);
}

public class RegularDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal totalAmount)
    {
        return totalAmount * 0.1m; // 10% discount for regular customers
    }
}

public class VIPDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal totalAmount)
    {
        return totalAmount * 0.2m; // 20% discount for VIP customers
    }
}

public class SuperVIPDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(decimal totalAmount)
    {
        return totalAmount * 0.3m; // 30% discount for super VIP customers
    }
}

public class DiscountService
{
    private readonly Dictionary<string, IDiscountStrategy> _strategies;

    public DiscountService()
    {
        _strategies = new Dictionary<string, IDiscountStrategy>
        {
            { "Regular", new RegularDiscountStrategy() },
            { "VIP", new VIPDiscountStrategy() },
            { "SuperVIP", new SuperVIPDiscountStrategy() }
        };
    }

    public decimal CalculateDiscount(decimal totalAmount, string customerType)
    {
        if (_strategies.TryGetValue(customerType, out var strategy))
        {
            return strategy.CalculateDiscount(totalAmount);
        }
        return 0; // No discount for other customer types
    }
}


// Advantages of OCP:
// Extensibility
// Decoupling 
// Maintainability


// Liskov Substitution Principle:
// Objects of a superclass should be replaceable with objects of a subclass without affecting
// application behavior.

// Web API development example: if Controller depends on an interface or a base class,
// every implementation of that abstraction must behave in ways the controller expects.

public interface IUser { }

public interface IVipUser : IUser
{
    public string GetVipStatus();
}

public class VipUser : IVipUser
{
    public string GetVipStatus()
    {
        return "VIP";
    }
}

public class RegularUser : IUser { }

// Advantages of LSP:
// Substitutability
// Polymorphism



// Interface Segregation Principle:
// Clients should not be forced to depend on interfaces/implement methods they do not use.

// Dependency Inversion Principle:
// High-level modules should not depend on low-level modules.
// Both should depend on abstractions.

// Implementation of DIP via Dependency Injection
public interface IUserService
{
    User? GetUser(string email, string password);
    User? GetUserByEmail(string email);
}