using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PcConfigurator.Api.Domain.Entities;

namespace PcConfigurator.Api.Infrastructure;

public interface IJwtProvider
{
    (string token, DateTime expiresAtUtc) Create(User user);
}

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _opts;

    public JwtProvider(IOptions<JwtOptions> opts) => _opts = opts.Value;

    public (string token, DateTime expiresAtUtc) Create(User user)
    {
        var expires = DateTime.UtcNow.AddMinutes(_opts.ExpiresMinutes);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("login", user.Login),
            new Claim("displayName", user.DisplayName)
        };

        if (user.IsAdmin)
        {
            claims.Add(new Claim("adm", "true"));
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }

        var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opts.Key)), SecurityAlgorithms.HmacSha256);
        var jwt = new JwtSecurityToken(
            issuer: _opts.Issuer,
            audience: _opts.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: creds
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return (token, expires);
    }
}
