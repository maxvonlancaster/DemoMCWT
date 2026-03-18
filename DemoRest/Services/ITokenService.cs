namespace DemoRest.Services;

public interface ITokenService
{
    string GenerateToken(string username);
}
