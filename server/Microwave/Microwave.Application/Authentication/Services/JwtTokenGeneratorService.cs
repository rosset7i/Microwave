using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microwave.Domain.Entities;

namespace Microwave.Application.Authentication.Services;

public class JwtTokenGeneratorService : IJwtTokenGeneratorService
{
    private readonly IConfiguration _configuration;

    public JwtTokenGeneratorService(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var key = _configuration.GetSection("JwtSettings:Secret").Value!;

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha512);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, "Admin"),
            new(ClaimTypes.Role, "User")
        };

        var securityToken = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(60),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}