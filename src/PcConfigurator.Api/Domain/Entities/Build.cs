using System.ComponentModel.DataAnnotations.Schema;

namespace PcConfigurator.Api.Domain.Entities;

public class Build
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = default!;
    public string Name { get; set; } = "My Build";
    public string? Description { get; set; }
    public Guid? CpuId { get; set; }
    [ForeignKey(nameof(CpuId))] public Cpu? Cpu { get; set; }
    public Guid? GpuId { get; set; }
    [ForeignKey(nameof(GpuId))] public Gpu? Gpu { get; set; }
    public Guid? MbId { get; set; }
    [ForeignKey(nameof(MbId))] public Mb? Mb { get; set; }
    public Guid? PsuId { get; set; }
    [ForeignKey(nameof(PsuId))] public Psu? Psu { get; set; }
    public Guid? CaseId { get; set; }
    [ForeignKey(nameof(CaseId))] public Case? Case { get; set; }
    public Guid? CoolingId { get; set; }
    public CoolingKind? CoolingKind { get; set; }
    public Guid? MemoryId { get; set; }
    [ForeignKey(nameof(MemoryId))] public Memory? Memory { get; set; }
    public ICollection<BuildSsd> Ssds { get; set; } = new List<BuildSsd>();
    public ICollection<BuildHdd> Hdds { get; set; } = new List<BuildHdd>();
    public bool IsPublic { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}