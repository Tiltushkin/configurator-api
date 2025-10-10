using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcConfigurator.Api.Contracts;
using PcConfigurator.Api.Domain.Entities;
using PcConfigurator.Api.Infrastructure;

namespace PcConfigurator.Api.Controllers;

[ApiController]
[Route("api/builds")]
[Authorize]
public class BuildsController : ControllerBase
{
    private readonly AppDbContext _db;

    public BuildsController(AppDbContext db) => _db = db;

    private Guid? TryGetUserId()
    {
        var sub = User.FindFirst("sub")?.Value
                  ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? User.FindFirst("userId")?.Value;
        if (Guid.TryParse(sub, out var id)) return id;
        return null;
    }

    // GET /api/builds?scope=mine|public|all&userId={guid?}
    [HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] string? scope = "mine",
        [FromQuery] Guid? userId = null,
        CancellationToken ct = default)
    {
        var meOpt = TryGetUserId();
        if (meOpt is null) return Unauthorized();
        var meId = meOpt.Value;
        IQueryable<Build> q = _db.Builds.AsNoTracking();

        switch ((scope ?? "mine").ToLowerInvariant())
        {
            case "mine":
                q = q.Where(b => b.OwnerId == meId);
                break;
            case "public":
                q = q.Where(b => b.IsPublic);
                if (userId.HasValue) q = q.Where(b => b.OwnerId == userId.Value);
                break;
            case "all":
                q = q.Where(b => b.IsPublic || b.OwnerId == meId);
                if (userId.HasValue) q = q.Where(b => b.OwnerId == userId.Value);
                break;
            default:
                q = q.Where(b => b.OwnerId == meId);
                break;
        }

        var rows = await q
            .OrderByDescending(b => b.CreatedAt)
            .Select(b => new BuildResponse(
                b.Id,
                b.OwnerId,
                b.Owner.DisplayName,
                b.Name,
                b.Description,
                b.CpuId,
                b.GpuId,
                b.MbId,
                b.PsuId,
                b.CaseId,
                b.CoolingId,
                b.MemoryId,
                b.Ssds.Select(s => s.Id).ToArray(),
                b.Hdds.Select(h => h.DriveId).ToArray(),
                b.IsPublic,
                b.CreatedAt,
                b.UpdatedAt))
            .ToListAsync(ct);

        return Ok(rows);
    }

    // GET /api/builds/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken ct = default)
    {
        var meOpt = TryGetUserId();
        if (meOpt is null) return Unauthorized();
        var meId = meOpt.Value;

        var b = await _db.Builds
            .AsNoTracking()
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (b is null) return NotFound();
        if (!(b.IsPublic || b.OwnerId == meId)) return Forbid();

        return Ok(new BuildResponse(b.Id, b.OwnerId, b.Owner.DisplayName, b.Name, b.Description, b.CpuId, b.GpuId, b.MbId, b.PsuId,
        b.CaseId, b.CoolingId, b.MemoryId,
        _db.Entry(b).Collection(x => x.Ssds).IsLoaded ? b.Ssds.Select(s => s.Id).ToArray() : _db.Entry(b).Collection(x => x.Ssds).Query().Select(s => s.Id).ToArray(),
        b.Hdds.Select(h => h.DriveId).ToArray(), b.IsPublic, b.CreatedAt, b.UpdatedAt));
    }

    // POST /api/builds
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BuildCreateRequest req, CancellationToken ct = default)
    {
        var meOpt = TryGetUserId();
        if (meOpt is null) return Unauthorized();
        var meId = meOpt.Value;

        var b = new Build
        {
            OwnerId = meId,
            Name = string.IsNullOrWhiteSpace(req.Name) ? "My Build" : req.Name!.Trim(),
            Description = req.Description,
            CpuId = req.CpuId,
            GpuId = req.GpuId,
            MbId  = req.MbId,
            PsuId = req.PsuId,
            CaseId = req.CaseId,
            MemoryId = req.MemoryId,
            IsPublic = req.IsPublic
        };

        if (req.CoolingId is Guid coolId)
        {
            var ck = await ResolveCoolingAsync(coolId, ct) ?? throw new ArgumentException("CoolingId not found");
            b.CoolingId = ck.id;
            b.CoolingKind = ck.kind;
        }

        if (req.SsdIds is { Length: > 0 })
        {
            var ssdIds = req.SsdIds.ToHashSet();
            var ssds = await _db.Ssds.Where(x => ssdIds.Contains(x.Id)).ToListAsync(ct);
            if (ssds.Count != ssdIds.Count) throw new ArgumentException("Some SSD ids not found");
            foreach (var s in ssds) b.Ssds.Add(s);
        }

        if (req.HddIds is { Length: > 0 })
        {
            var links = await ResolveHddsAsync(req.HddIds, ct);
            if (links.Count != req.HddIds.Distinct().Count()) throw new ArgumentException("Some HDD ids not found");
            foreach (var l in links) b.Hdds.Add(l);
        }

        _db.Builds.Add(b);
        await _db.SaveChangesAsync(ct);
        await _db.Entry(b).Reference(x => x.Owner).LoadAsync(ct);

        return Ok(new BuildResponse(b.Id, b.OwnerId, b.Owner.DisplayName, b.Name, b.Description, b.CpuId, b.GpuId, b.MbId, b.PsuId,
        b.CaseId, b.CoolingId, b.MemoryId,
        _db.Entry(b).Collection(x => x.Ssds).IsLoaded ? b.Ssds.Select(s => s.Id).ToArray() : _db.Entry(b).Collection(x => x.Ssds).Query().Select(s => s.Id).ToArray(),
        b.Hdds.Select(h => h.DriveId).ToArray(), b.IsPublic, b.CreatedAt, b.UpdatedAt));
    }

    // PUT /api/builds/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] BuildUpdateRequest req, CancellationToken ct = default)
    {
        var meOpt = TryGetUserId();
        if (meOpt is null) return Unauthorized();
        var meId = meOpt.Value;

        var b = await _db.Builds.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (b is null) return NotFound();
        if (b.OwnerId != meId) return Forbid();

        if (!string.IsNullOrWhiteSpace(req.Name))
            b.Name = req.Name!.Trim();

        b.Description = req.Description;
        b.CpuId = req.CpuId;
        b.GpuId = req.GpuId;
        b.MbId = req.MbId;
        b.PsuId = req.PsuId;
        b.CaseId = req.CaseId;
        b.MemoryId = req.MemoryId;
        b.IsPublic = req.IsPublic;

        if (req.CoolingId is Guid coolId)
        {
            var ck = await ResolveCoolingAsync(coolId, ct) ?? throw new ArgumentException("CoolingId not found");
            b.CoolingId = ck.id;
            b.CoolingKind = ck.kind;
        }

        if (req.SsdIds is { Length: > 0 })
        {
            var ssdIds = req.SsdIds.ToHashSet();
            var ssds = await _db.Ssds.Where(x => ssdIds.Contains(x.Id)).ToListAsync(ct);
            if (ssds.Count != ssdIds.Count) throw new ArgumentException("Some SSD ids not found");
            foreach (var s in ssds) b.Ssds.Add(s);
        }

        if (req.HddIds is { Length: > 0 })
        {
            var links = await ResolveHddsAsync(req.HddIds, ct);
            if (links.Count != req.HddIds.Distinct().Count()) throw new ArgumentException("Some HDD ids not found");
            foreach (var l in links) b.Hdds.Add(l);
        }

        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    // DELETE /api/builds/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct = default)
    {
        var meOpt = TryGetUserId();
        if (meOpt is null) return Unauthorized();
        var meId = meOpt.Value;

        var b = await _db.Builds.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (b is null) return NotFound();
        if (b.OwnerId != meId) return Forbid();

        _db.Builds.Remove(b);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    // POST /api/builds/{id}/share?expireDays=30
    [HttpPost("{id:guid}/share")]
    public async Task<IActionResult> Share([FromRoute] Guid id, [FromQuery] int? expireDays, CancellationToken ct = default)
    {
        var meOpt = TryGetUserId();
        if (meOpt is null) return Unauthorized();
        var meId = meOpt.Value;

        var b = await _db.Builds.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (b is null) return NotFound();
        if (b.OwnerId != meId) return Forbid();

        var token = Convert.ToHexString(Guid.NewGuid().ToByteArray()).ToLowerInvariant();

        var share = new BuildShare
        {
            BuildId = b.Id,
            Token = token,
            ExpiresAt = expireDays.HasValue ? DateTime.UtcNow.AddDays(expireDays.Value) : null
        };

        _db.BuildShares.Add(share);
        await _db.SaveChangesAsync(ct);

        var url = $"{Request.Scheme}://{Request.Host}/api/builds/shared/{token}";
        return Ok(new ShareResponse(token, url, share.ExpiresAt));
    }

    // GET /api/builds/shared/{token}
    [AllowAnonymous]
    [HttpGet("shared/{token}")]
    public async Task<IActionResult> GetShared([FromRoute] string token, CancellationToken ct = default)
    {
        var s = await _db.BuildShares
            .Include(x => x.Build)
            .ThenInclude(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Token == token, ct);

        if (s is null) return NotFound();
        if (s.ExpiresAt.HasValue && s.ExpiresAt.Value < DateTime.UtcNow) return NotFound();

        var b = s.Build!;
        return Ok(new BuildResponse(b.Id, b.OwnerId, b.Owner.DisplayName, b.Name, b.Description, b.CpuId, b.GpuId, b.MbId, b.PsuId,
        b.CaseId, b.CoolingId, b.MemoryId,
        _db.Entry(b).Collection(x => x.Ssds).IsLoaded ? b.Ssds.Select(s => s.Id).ToArray() : _db.Entry(b).Collection(x => x.Ssds).Query().Select(s => s.Id).ToArray(),
        b.Hdds.Select(h => h.DriveId).ToArray(), b.IsPublic, b.CreatedAt, b.UpdatedAt));
    }

    private async Task<(Guid id, CoolingKind kind)?> ResolveCoolingAsync(Guid id, CancellationToken ct)
    {
        if (await _db.AirCoolings.AnyAsync(x => x.Id == id, ct))
            return (id, CoolingKind.AirCooling);
        if (await _db.Szos.AnyAsync(x => x.Id == id, ct))
            return (id, CoolingKind.Szo);
        return null;
    }

    private async Task<List<BuildHdd>> ResolveHddsAsync(IEnumerable<Guid> ids, CancellationToken ct)
    {
        var res = new List<BuildHdd>();
        var set = ids.ToHashSet();

        var h25 = await _db.Hdd2_5s.Where(x => set.Contains(x.Id)).Select(x => x.Id).ToListAsync(ct);
        res.AddRange(h25.Select(id => new BuildHdd { DriveId = id, DriveKind = HddKind.Hdd2_5 }));

        var h35 = await _db.Hdd3_5s.Where(x => set.Contains(x.Id)).Select(x => x.Id).ToListAsync(ct);
        res.AddRange(h35.Select(id => new BuildHdd { DriveId = id, DriveKind = HddKind.Hdd3_5 }));

        return res;
    }
}