using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class Psu
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    public string Model { get; set; } = null!;
    public int Power { get; set; }
    public string FormFactor { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string DetachableCables { get; set; } = string.Empty;
    public string CablesColor { get; set; } = string.Empty;
    public string BacklightType { get; set; } = string.Empty;
    public string[] MainPowerConnector { get; set; } = Array.Empty<string>();
    public string[] ProcessorPowerConnectors { get; set; } = Array.Empty<string>();
    public bool Floppy4PinConnector { get; set; }
    public string[] VideoCardPowerConnectors { get; set; } = Array.Empty<string>();
    public int SATAConnectors { get; set; }
    public int MolexConnectors { get; set; }
    public int MainPowerCableLength { get; set; }
    public int ProcessorPowerCableLength { get; set; }
    public int PCIEPowerCableLength { get; set; }
    public int SATAPowerCableLength { get; set; }
    public int MolexPowerCableLength { get; set; }
    public int PowerV12Line { get; set; }
    public int VoltageV12Line { get; set; }
    public int VoltageV3_3Line { get; set; }
    public int VoltageV5Line { get; set; }
    public int StandbyPowerSupply { get; set; }
    public int VoltageMinusV12 { get; set; }
    public string InputVoltageRange { get; set; } = string.Empty;
    public string CoolingType { get; set; } = string.Empty;
    public string FanDimensions { get; set; } = string.Empty;
    public string Certificate { get; set; } = string.Empty;
    public string PFC { get; set; } = string.Empty;
    public string[] ComplianceWithStandards { get; set; } = Array.Empty<string>();
    public string[] ProtectionTechnologies { get; set; } = Array.Empty<string>();
    public int Width { get; set; }
    public int Height { get; set; }
    public int Thickness { get; set; }
}
