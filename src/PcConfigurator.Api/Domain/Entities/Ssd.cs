using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class Ssd
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    [MaxLength(128)] public string Model { get; set; } = default!;
    [MaxLength(64)] public string ManufacturerCode { get; set; } = default!;
    public int DiskCapacity { get; set; }
    [MaxLength(64)] public string FormFactor { get; set; } = default!;
    [MaxLength(64)] public string PhysInterface { get; set; } = default!;
    [MaxLength(64)] public string M2ConnectorKey { get; set; } = default!;
    public bool NVMe { get; set; }
    [MaxLength(64)] public string MemoryStructure { get; set; } = default!;
    public bool DRAM { get; set; }
    public int MaxReadSpeed { get; set; }
    public int MaxWriteSpeed { get; set; }
    public int TBW { get; set; }
    public int DWPD { get; set; }
    public bool Radiator { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Thickness { get; set; }
    public int Weight { get; set; }
}