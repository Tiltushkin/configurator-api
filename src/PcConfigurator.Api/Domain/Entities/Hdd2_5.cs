using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class Hdd2_5
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    [MaxLength(128)] public string Model { get; set; } = default!;
    [MaxLength(64)] public string ManufacturerCode { get; set; } = default!;
    public int DiskCapacity { get; set; }
    public int BufferSize { get; set; }
    public int SpindleRotationSpeed { get; set; }
    public int AVGAccessReading { get; set; }
    [MaxLength(64)] public string Interface { get; set; } = default!;
    public int InterfaceBandwidth { get; set; }
    [MaxLength(64)] public string RecordingTechnology { get; set; } = default!;
    public int ImpactResistance { get; set; }
    public int ParkingCycles { get; set; }
    public int MinWorkingTemp { get; set; }
    public int MaxWorkingTemp { get; set; }
    public int Width { get; set; }
    public int Length { get; set; }
    public int StandardThickness { get; set; }
    public int Weight { get; set; }
}