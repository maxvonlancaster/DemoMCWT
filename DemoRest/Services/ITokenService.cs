using DemoRest.Models;

namespace DemoRest.Services;

public interface ITokenService
{
    string GenerateToken(string username);
    //string GetToken(string token);
    

}

public interface ITokenValidator 
{
    string? ValidateToken(string token);
}

public interface IUserService
{
    User? GetUser(string username);
}