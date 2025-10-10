using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcConfigurator.Api.Contracts;
using PcConfigurator.Api.Infrastructure;

namespace PcConfigurator.Api.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfilesController : ControllerBase
{
    private readonly AppDbContext _db;

    public ProfilesController(AppDbContext db) => _db = db;

    private Guid? TryGetUserId()
    {
        var sub = User.FindFirst("sub")?.Value
                  ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? User.FindFirst("userId")?.Value;
        if (Guid.TryParse(sub, out var id)) return id;
        return null;
    }

    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(ProfileResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Me(CancellationToken ct)
    {
        var id = TryGetUserId();
        if (id is null) return Unauthorized();

        var u = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id.Value, ct);
        if (u is null) return NotFound();

        return Ok(new ProfileResponse(u.Id, u.Login, u.DisplayName, u.AvatarUrl, u.CreatedAt));
    }

    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(ProfileResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById([FromRoute] Guid userId, CancellationToken ct)
    {
        var u = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId, ct);
        if (u is null) return NotFound();
        return Ok(new ProfileResponse(u.Id, u.Login, u.DisplayName, u.AvatarUrl, u.CreatedAt));
    }

    [Authorize]
    [HttpPut("me")]
    public async Task<IActionResult> UpdateMe([FromBody] ProfileUpdateRequest req, CancellationToken ct)
    {
        var id = TryGetUserId();
        if (id is null) return Unauthorized();

        var u = await _db.Users.FirstOrDefaultAsync(x => x.Id == id.Value, ct);
        if (u is null) return NotFound();

        u.DisplayName = req.UserName.Trim();
        u.AvatarUrl = req.AvatarUrl.Trim();
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }
}
