using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class Szo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    [MaxLength(128)] public string Model { get; set; } = default!;
    [MaxLength(64)] public string MainColor { get; set; } = default!;
    [MaxLength(64)] public string BackLightType { get; set; } = default!;
    [MaxLength(64)] public string BackLightSource { get; set; } = default!;
    [MaxLength(64)] public string BackLightConnector { get; set; } = default!;
    public bool LCD { get; set; }
    [MaxLength(64)] public string Purpose { get; set; } = default!;
    [MaxLength(64)] public string WaterBlockMaterial { get; set; } = default!;
    [MaxLength(64)] public string WaterBlockSize { get; set; } = default!;
    public string[] Sockets { get; set; } = Array.Empty<string>();
    [MaxLength(64)] public string RadiatorMountingDimensions { get; set; } = default!;
    public int TDP { get; set; }
    public int RadiatorLength { get; set; }
    public int RadiatorWidth { get; set; }
    public int RadiatorThickness { get; set; }
    [MaxLength(64)] public string RadiatorMaterial { get; set; } = default!;
    public int IncludedFans { get; set; }
    [MaxLength(64)] public string FanDimensions { get; set; } = default!;
    [MaxLength(64)] public string FanBearingType { get; set; } = default!;
    public int MinRotationSpeed { get; set; }
    public int MaxRotationSpeed { get; set; }
    [MaxLength(64)] public string FanSpeedAdjustment { get; set; } = default!;
    public int MaxNoiseLevel { get; set; }
    public int MaxAirFlow { get; set; }
    public int MaxStaticPressure { get; set; }
    [MaxLength(64)] public string FanConnectionSocket { get; set; } = default!;
    public int PumpNoiseLevel { get; set; }
    public int PumpRotationSpeed { get; set; }
    [MaxLength(64)] public string PumpConnectionSocket { get; set; } = default!;
    public int TubeLength { get; set; }
}