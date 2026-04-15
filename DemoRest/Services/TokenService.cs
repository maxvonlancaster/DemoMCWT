using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using DemoRest.Models;

namespace DemoRest.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var secretKey = _configuration["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //public string GetToken(string token)
    //{
    //    throw new NotImplementedException();
    //}

    //public string GetToken(string token, string username)
    //{
    //    throw new NotImplementedException();
    //}

    //public User GetUser(string username)
    //{
    //    throw new NotImplementedException();
    //}

    //public string? ValidateToken(string token)
    //{
    //    throw new NotImplementedException();
    //}
}

public class TokenValidator : ITokenValidator
{
    private readonly IConfiguration _configuration;
    public TokenValidator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = _configuration["Jwt:Key"];
        var key = Encoding.UTF8.GetBytes(secretKey);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out SecurityToken validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var username = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            return username;
        }
        catch
        {
            return null;
        }
    }
}