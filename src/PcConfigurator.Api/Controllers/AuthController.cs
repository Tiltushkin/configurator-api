using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcConfigurator.Api.Contracts;
using PcConfigurator.Api.Domain.Entities;
using PcConfigurator.Api.Infrastructure;

namespace PcConfigurator.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IJwtProvider _jwt;
    private const string DefaultAvatar = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png";

    public AuthController(AppDbContext db, IJwtProvider jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest req, CancellationToken ct)
    {
        var login = req.Login.Trim();
        var exists = await _db.Users.AnyAsync(u => u.Login == login, ct);
        if (exists) return Conflict(new { message = "Login already registered" });

        var user = new User
        {
            Login = login,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.Password),
            DisplayName = string.IsNullOrWhiteSpace(req.DisplayName) ? login : req.DisplayName!.Trim(),
            AvatarUrl = DefaultAvatar
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync(ct);

        var (token, exp) = _jwt.Create(user);
        return Ok(new TokenResponse(token, exp));
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest req, CancellationToken ct)
    {
        var login = req.Login.Trim();
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Login == login, ct);
        if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
            return Unauthorized(new { message = "Invalid credentials" });

        var (token, exp) = _jwt.Create(user);
        return Ok(new TokenResponse(token, exp));
    }
}
