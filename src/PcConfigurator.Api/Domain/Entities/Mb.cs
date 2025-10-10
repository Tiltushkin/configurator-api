using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PcConfigurator.Api.Domain.Entities;

public class Mb
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();

    public decimal Price { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();
    public int Warranty { get; set; }
    public string Model { get; set; } = null!;
    public string Series { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string[] FormFactor { get; set; } = Array.Empty<string>();
    public int Width { get; set; }
    public int Height { get; set; }
    public string Socket { get; set; } = string.Empty;
    public string CheapSet { get; set; } = string.Empty;
    public string[] MemoryTypes { get; set; } = Array.Empty<string>();
    public int MaxMemory { get; set; }
    public string[] MemoryFormFactor { get; set; } = Array.Empty<string>();
    public int MemorySlots { get; set; }
    public int MemoryCanals { get; set; }
    public int MaxMemoryFrequency { get; set; }
    public int[] MaxMemoryBoostFrequency { get; set; } = Array.Empty<int>();
    public string PCIExpress { get; set; } = string.Empty;
    public string[] PCISlots { get; set; } = Array.Empty<string>();
    public bool SLI { get; set; }
    public int CardsInSLI { get; set; }
    public int PCIEx1Slots { get; set; }
    public bool NVMeSupport { get; set; }
    public string DiskPCIExpress { get; set; } = string.Empty;
    public int M2Slots { get; set; }
    public string[] M2ConnectorsPCIeProcessor { get; set; } = Array.Empty<string>();
    public string[] M2ConnectorsPCIeCheapSet { get; set; } = Array.Empty<string>();
    public int SATAPorts { get; set; }
    public string[] SATARAID { get; set; } = Array.Empty<string>();
    public string[] NVMeRAID { get; set; } = Array.Empty<string>();
    public string? PortsJson { get; set; }

    public string[] OutUSBTypeA { get; set; } = Array.Empty<string>();
    public string[] OutUSBTypeC { get; set; } = Array.Empty<string>();
    public string[] InUSBTypeA { get; set; } = Array.Empty<string>();
    public string[] InUSBTypeC { get; set; } = Array.Empty<string>();
    public string[] VideoOutputs { get; set; } = Array.Empty<string>();
    public int NetworkPorts { get; set; }
    public string[] ProcessorCoolingConnectors { get; set; } = Array.Empty<string>();
    public int SZOConnectors { get; set; }
    public int CaseFansConnectors4Pin { get; set; }
    public int CaseFansConnectors3Pin { get; set; }
    public int VDGConnectors { get; set; }
    public int VGRBConnectors { get; set; }
    public bool M2Wireless { get; set; }
    public bool RS232 { get; set; }
    public bool LPT { get; set; }
    public string MainPowerConnector { get; set; } = string.Empty;
    public string ProcessorPowerConnector { get; set; } = string.Empty;
    public string PowerPhases { get; set; } = string.Empty;
    public string[] PassiveCooling { get; set; } = Array.Empty<string>();
}
