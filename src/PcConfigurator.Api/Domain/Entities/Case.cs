using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class Case
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    [MaxLength(128)] public string Model { get; set; } = default!;
    [MaxLength(64)] public string ManufacturerCode { get; set; } = default!;
    [MaxLength(64)] public string BodySize { get; set; } = default!;
    [MaxLength(64)] public string MotherBoardOrientation { get; set; } = default!;
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    [MaxLength(64)] public string MainColor { get; set; } = default!;
    public string[] BodyMaterial { get; set; } = Array.Empty<string>();
    public bool SideWallWindow { get; set; }
    [MaxLength(64)] public string WindowMaterial { get; set; } = default!;
    public string[] FrontPanelMaterial { get; set; } = Array.Empty<string>();
    [MaxLength(64)] public string BackLightType { get; set; } = default!;
    [MaxLength(64)] public string BackLightColor { get; set; } = default!;
    [MaxLength(64)] public string BackLightSource { get; set; } = default!;
    [MaxLength(64)] public string BackLightConnector { get; set; } = default!;
    public string[] BackLightControl { get; set; } = Array.Empty<string>();
    public string[] CompatibleBoards { get; set; } = Array.Empty<string>();
    public string[] CompatiblePowerSupply { get; set; } = Array.Empty<string>();
    [MaxLength(64)] public string PowerSupplyPlacement { get; set; } = default!;
    public int PowerSupplyLength { get; set; }
    public int HorizontalExpansionSlots { get; set; }
    public int GPULength { get; set; }
    public int MaxCoolerHeight { get; set; }
    public int Drives2_5 { get; set; }
    public int InternalCompartments3_5 { get; set; }
    public int ExternalCompartments3_5 { get; set; }
    public int Drives5_25 { get; set; }
    public string[] IncludedFans { get; set; } = Array.Empty<string>();
    public string[] RearFanSupport { get; set; } = Array.Empty<string>();
    public string[] TopFansSupport { get; set; } = Array.Empty<string>();
    public string[] BottomFansSupport { get; set; } = Array.Empty<string>();
    public string[] SideFansSupport { get; set; } = Array.Empty<string>();
    public bool SZOSupport { get; set; }
    public string[] SZOUpperMountingDimension { get; set; } = Array.Empty<string>();
    public string[] SZORearMountingDimension { get; set; } = Array.Empty<string>();
    public string[] SZOSideMountingDimension { get; set; } = Array.Empty<string>();
}