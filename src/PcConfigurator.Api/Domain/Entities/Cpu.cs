using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class Cpu
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    [MaxLength(128)] public string Model { get; set; } = default!;
    [MaxLength(64)] public string Socket { get; set; } = default!;
    [MaxLength(64)] public string ManufacturerCode { get; set; } = default!;
    public int ReleaseYear { get; set; }
    public bool CoolingSystem { get; set; }
    public bool ThermalInterface { get; set; }
    public int TotalCores { get; set; }
    public int ProductiveCores { get; set; }
    public int EnergyEfficientCores { get; set; }
    public int Threads { get; set; }
    public int CacheL1 { get; set; }
    public int CacheL2 { get; set; }
    public int CacheL3 { get; set; }
    [MaxLength(64)] public string TechnicalProcess { get; set; } = default!;
    [MaxLength(64)] public string Core { get; set; } = default!;
    public decimal BasicFrequency { get; set; }
    public decimal TurboFrequency { get; set; }
    public decimal EnergyEfficientBasicFrequency { get; set; }
    public decimal EnergyEfficientTurboFrequency { get; set; }
    public bool FreeMultiplier { get; set; }
    public string[] MemoryTypes { get; set; } = Array.Empty<string>();
    public int MaxMemory { get; set; }
    public int Canals { get; set; }
    public string[] MemoryFrequency { get; set; } = Array.Empty<string>();
    public bool ECCMode { get; set; }
    public int TDP { get; set; }
    public int BasicHeatProduction { get; set; }
    public int MaxTemp { get; set; }
    public bool GraphicalCore { get; set; }
    [MaxLength(64)] public string PCIExpress { get; set; } = default!;
    public int PCIExpressLines { get; set; }
    public bool Virtualization { get; set; }
    public string[] Additionally { get; set; } = Array.Empty<string>();
}
