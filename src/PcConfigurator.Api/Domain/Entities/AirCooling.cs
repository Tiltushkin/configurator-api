using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class AirCooling
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    [MaxLength(128)] public string Model { get; set; } = default!;
    [MaxLength(64)] public string ManufacturerCode { get; set; } = default!;
    public string[] Sockets { get; set; } = Array.Empty<string>();
    public int TDP { get; set; }
    [MaxLength(64)] public string ConstructionType { get; set; } = default!;
    [MaxLength(64)] public string BaseMaterial { get; set; } = default!;
    [MaxLength(64)] public string RadiatorMaterial { get; set; } = default!;
    public int HeatPipes { get; set; }
    public int HeatPipeDiameter { get; set; }
    [MaxLength(64)] public string NickelPlatedCoating { get; set; } = default!;
    [MaxLength(64)] public string RadiatorColor { get; set; } = default!;
    public int FansIncluded { get; set; }
    public int MaxFans { get; set; }
    [MaxLength(64)] public string FansSize { get; set; } = default!;
    [MaxLength(64)] public string FanColor { get; set; } = default!;
    [MaxLength(64)] public string FanConnector { get; set; } = default!;
    public int MinRotationSpeed { get; set; }
    public int MaxRotationSpeed { get; set; }
    [MaxLength(64)] public string RotationAdjustment { get; set; } = default!;
    public int AirFlow { get; set; }
    public int MaxStaticPressure { get; set; }
    public int MaxNoiseLevel { get; set; }
    public int RatedCurrent { get; set; }
    public int RatedVoltage { get; set; }
    [MaxLength(64)] public string BearingType { get; set; } = default!;
    public int Height { get; set; }
    public int Length { get; set; }
    public int Width { get; set; }
    public int Weight { get; set; }
    public string[] Additionally { get; set; } = Array.Empty<string>();
}