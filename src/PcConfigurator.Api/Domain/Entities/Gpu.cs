using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class Gpu
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    [MaxLength(8)] public string Type { get; set; } = "GPU";
    [MaxLength(128)] public string Model { get; set; } = default!;
    [MaxLength(64)] public string ManufacturerCode { get; set; } = default!;
    [MaxLength(64)] public string Color { get; set; } = default!;
    public bool CanMining { get; set; }
    public bool LHR { get; set; }
    [MaxLength(64)] public string GraphicalProcessor { get; set; } = default!;
    [MaxLength(64)] public string MicroArch { get; set; } = default!;
    public int TechProcess { get; set; }
    public int BasicFrequency { get; set; }
    public int TurboFrequency { get; set; }
    public int ALU { get; set; }
    public int TextureBlocks { get; set; }
    public int RasterizationBlocks { get; set; }
    public bool RayTracing { get; set; }
    public int RtCores { get; set; }
    public int TensorCores { get; set; }
    public int VideoRAM { get; set; }
    [MaxLength(64)] public string MemoryType { get; set; } = default!;
    public int MemoryBusWidth { get; set; }
    public int MemoryBandwidth { get; set; }
    public int MemoryFrequency { get; set; }
    public string[] VideoConnectors { get; set; } = Array.Empty<string>();
    [MaxLength(16)] public string HDMIVersion { get; set; } = default!;
    [MaxLength(16)] public string DisplayPortVersion { get; set; } = default!;
    public int MaximumMonitors { get; set; }
    [MaxLength(32)] public string MaximumResolution { get; set; } = default!;
    [MaxLength(64)] public string ConnectionInterface { get; set; } = default!;
    [MaxLength(64)] public string ConnectionFormFactor { get; set; } = default!;
    public int PCIExpressLines { get; set; }
    [MaxLength(64)] public string PowerConnections { get; set; } = default!;
    public int RecommendedPowerSupply { get; set; }
    public int PowerConsumption { get; set; }
    [MaxLength(32)] public string CoolingType { get; set; } = default!;
    [MaxLength(32)] public string NumberOfFans { get; set; } = default!;
    public bool LiquidCoolingRadiator { get; set; }
    public bool Backlight { get; set; }
    public bool RGBSync { get; set; }
    public bool LCD { get; set; }
    public bool BiosToggler { get; set; }
    public string[] Completion { get; set; } = Array.Empty<string>();
    public string[] Additionally { get; set; } = Array.Empty<string>();
    public bool LowProfile { get; set; }
    public int ExpansionSlots { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Thickness { get; set; }
    public int Weight { get; set; }
}
