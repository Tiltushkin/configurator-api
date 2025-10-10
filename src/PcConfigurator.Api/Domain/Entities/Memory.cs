using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class Memory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    [MaxLength(128)] public string Model { get; set; } = default!;
    [MaxLength(64)] public string ManufacturerCode { get; set; } = default!;
    [MaxLength(64)] public string MemoryType { get; set; } = default!;
    [MaxLength(64)] public string MemoryModuleType { get; set; } = default!;
    public int TotalMemory { get; set; }
    public int OneModuleMemory { get; set; }
    public int TotalModules { get; set; }
    public bool RegisterMemory { get; set; }
    public bool ECCMemory { get; set; }
    [MaxLength(64)] public string Ranking { get; set; } = default!;
    public int ClockFrequency { get; set; }
    public string[] AMDExpo { get; set; } = Array.Empty<string>();
    public string[] IntelXMP { get; set; } = Array.Empty<string>();
    public int CL { get; set; }
    public int TRCD { get; set; }
    public int TRP { get; set; }
    public bool Radiator { get; set; }
    [MaxLength(64)] public string RadiatorColor { get; set; } = default!;
    [MaxLength(64)] public string BoardElementBacklight { get; set; } = default!;
    public int Height { get; set; }
    public bool LowProfile { get; set; }
    public int Voltage { get; set; }
    public string[] Additionally { get; set; } = Array.Empty<string>();
}