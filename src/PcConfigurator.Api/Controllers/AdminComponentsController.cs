using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcConfigurator.Api.Contracts;
using PcConfigurator.Api.Domain.Entities;
using PcConfigurator.Api.Infrastructure;

namespace PcConfigurator.Api.Controllers;

[ApiController]
[Route("api/admin/components")]
[Authorize(Policy = "Admin")]
public class AdminComponentsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly JsonSerializerOptions _json;

    public AdminComponentsController(AppDbContext db)
    {
        _db = db;
        _json = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
    }

    [HttpPost("cpu")]
    public async Task<IActionResult> AddCpu([FromBody] CpuCreateRequest req, CancellationToken ct)
    {
        var e = new Cpu
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            Socket = req.Socket,
            ManufacturerCode = req.ManufacturerCode,
            ReleaseYear = req.ReleaseYear,
            CoolingSystem = req.CoolingSystem,
            ThermalInterface = req.ThermalInterface,
            TotalCores = req.TotalCores,
            ProductiveCores = req.ProductiveCores,
            EnergyEfficientCores = req.EnergyEfficientCores,
            Threads = req.Threads,
            CacheL1 = req.CacheL1,
            CacheL2 = req.CacheL2,
            CacheL3 = req.CacheL3,
            TechnicalProcess = req.TechnicalProcess,
            Core = req.Core,
            BasicFrequency = req.BasicFrequency,
            TurboFrequency = req.TurboFrequency,
            EnergyEfficientBasicFrequency = req.EnergyEfficientBasicFrequency,
            EnergyEfficientTurboFrequency = req.EnergyEfficientTurboFrequency,
            FreeMultiplier = req.FreeMultiplier,
            MemoryTypes = req.MemoryTypes ?? Array.Empty<string>(),
            MaxMemory = req.MaxMemory,
            Canals = req.Canals,
            MemoryFrequency = req.MemoryFrequency ?? Array.Empty<string>(),
            ECCMode = req.ECCMode,
            TDP = req.TDP,
            BasicHeatProduction = req.BasicHeatProduction,
            MaxTemp = req.MaxTemp,
            GraphicalCore = req.GraphicalCore,
            PCIExpress = req.PCIExpress,
            PCIExpressLines = req.PCIExpressLines,
            Virtualization = req.Virtualization,
            Additionally = req.Additionally ?? Array.Empty<string>()
        };

        _db.Cpus.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("gpu")]
    public async Task<IActionResult> AddGpu([FromBody] GpuCreateRequest req, CancellationToken ct)
    {
        var e = new Gpu
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            ManufacturerCode = req.ManufacturerCode,
            Color = req.Color,
            CanMining = req.CanMining,
            LHR = req.LHR,
            GraphicalProcessor = req.GraphicalProcessor,
            MicroArch = req.MicroArch,
            TechProcess = req.TechProcess,
            BasicFrequency = req.BasicFrequency,
            TurboFrequency = req.TurboFrequency,
            ALU = req.ALU,
            TextureBlocks = req.TextureBlocks,
            RasterizationBlocks = req.RasterizationBlocks,
            RayTracing = req.RayTracing,
            RtCores = req.RtCores,
            TensorCores = req.TensorCores,
            VideoRAM = req.VideoRAM,
            MemoryType = req.MemoryType,
            MemoryBusWidth = req.MemoryBusWidth,
            MemoryBandwidth = req.MemoryBandwidth,
            MemoryFrequency = req.MemoryFrequency,
            VideoConnectors = req.VideoConnectors ?? Array.Empty<string>(),
            HDMIVersion = req.HDMIVersion,
            DisplayPortVersion = req.DisplayPortVersion,
            MaximumMonitors = req.MaximumMonitors,
            MaximumResolution = req.MaximumResolution,
            ConnectionInterface = req.ConnectionInterface,
            ConnectionFormFactor = req.ConnectionFormFactor,
            PCIExpressLines = req.PCIExpressLines,
            PowerConnections = req.PowerConnections,
            RecommendedPowerSupply = req.RecommendedPowerSupply,
            PowerConsumption = req.PowerConsumption,
            CoolingType = req.CoolingType,
            NumberOfFans = req.NumberOfFans,
            LiquidCoolingRadiator = req.LiquidCoolingRadiator,
            Backlight = req.Backlight,
            RGBSync = req.RGBSync,
            LCD = req.LCD,
            BiosToggler = req.BiosToggler,
            Completion = req.Completion ?? Array.Empty<string>(),
            Additionally = req.Additionally ?? Array.Empty<string>(),
            LowProfile = req.LowProfile,
            ExpansionSlots = req.ExpansionSlots,
            Width = req.Width,
            Height = req.Height,
            Thickness = req.Thickness,
            Weight = req.Weight
        };

        _db.Gpus.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("mb")]
    public async Task<IActionResult> AddMb([FromBody] MbCreateRequest req, CancellationToken ct)
    {
        var e = new Mb
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            Series = req.Series,
            Color = req.Color,
            ReleaseYear = req.ReleaseYear,
            FormFactor = req.FormFactor ?? Array.Empty<string>(),
            Width = req.Width,
            Height = req.Height,
            Socket = req.Socket,
            CheapSet = req.CheapSet,
            MemoryTypes = req.MemoryTypes ?? Array.Empty<string>(),
            MaxMemory = req.MaxMemory,
            MemoryFormFactor = req.MemoryFormFactor ?? Array.Empty<string>(),
            MemorySlots = req.MemorySlots,
            MemoryCanals = req.MemoryCanals,
            MaxMemoryFrequency = req.MaxMemoryFrequency,
            MaxMemoryBoostFrequency = req.MaxMemoryBoostFrequency ?? Array.Empty<int>(),
            PCIExpress = req.PCIExpress,
            PCISlots = req.PCISlots ?? Array.Empty<string>(),
            SLI = req.SLI,
            CardsInSLI = req.CardsInSLI,
            PCIEx1Slots = req.PCIEx1Slots,
            NVMeSupport = req.NVMeSupport,
            DiskPCIExpress = req.DiskPCIExpress,
            M2Slots = req.M2Slots,
            M2ConnectorsPCIeProcessor = req.M2ConnectorsPCIeProcessor ?? Array.Empty<string>(),
            M2ConnectorsPCIeCheapSet = req.M2ConnectorsPCIeCheapSet ?? Array.Empty<string>(),
            SATAPorts = req.SATAPorts,
            SATARAID = req.SATARAID ?? Array.Empty<string>(),
            NVMeRAID = req.NVMeRAID ?? Array.Empty<string>(),
            OutUSBTypeA = req.OutUSBTypeA ?? Array.Empty<string>(),
            OutUSBTypeC = req.OutUSBTypeC ?? Array.Empty<string>(),
            InUSBTypeA = req.InUSBTypeA ?? Array.Empty<string>(),
            InUSBTypeC = req.InUSBTypeC ?? Array.Empty<string>(),
            VideoOutputs = req.VideoOutputs ?? Array.Empty<string>(),
            NetworkPorts = req.NetworkPorts,
            ProcessorCoolingConnectors = req.ProcessorCoolingConnectors ?? Array.Empty<string>(),
            SZOConnectors = req.SZOConnectors,
            CaseFansConnectors4Pin = req.CaseFansConnectors4Pin,
            CaseFansConnectors3Pin = req.CaseFansConnectors3Pin,
            VDGConnectors = req.VDGConnectors,
            VGRBConnectors = req.VGRBConnectors,
            M2Wireless = req.M2Wireless,
            RS232 = req.RS232,
            LPT = req.LPT,
            MainPowerConnector = req.MainPowerConnector,
            ProcessorPowerConnector = req.ProcessorPowerConnector,
            PowerPhases = req.PowerPhases,
            PassiveCooling = req.PassiveCooling ?? Array.Empty<string>()
        };

        _db.Mbs.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("psu")]
    public async Task<IActionResult> AddPsu([FromBody] PsuCreateRequest req, CancellationToken ct)
    {
        var e = new Psu
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            Power = req.Power,
            FormFactor = req.FormFactor,
            Color = req.Color,
            DetachableCables = req.DetachableCables,
            CablesColor = req.CablesColor,
            BacklightType = req.BacklightType,
            MainPowerConnector = req.MainPowerConnector ?? Array.Empty<string>(),
            ProcessorPowerConnectors = req.ProcessorPowerConnectors ?? Array.Empty<string>(),
            Floppy4PinConnector = req.Floppy4PinConnector,
            VideoCardPowerConnectors = req.VideoCardPowerConnectors ?? Array.Empty<string>(),
            SATAConnectors = req.SATAConnectors,
            MolexConnectors = req.MolexConnectors,
            MainPowerCableLength = req.MainPowerCableLength,
            ProcessorPowerCableLength = req.ProcessorPowerCableLength,
            PCIEPowerCableLength = req.PCIEPowerCableLength,
            SATAPowerCableLength = req.SATAPowerCableLength,
            MolexPowerCableLength = req.MolexPowerCableLength,
            PowerV12Line = req.PowerV12Line,
            VoltageV12Line = req.VoltageV12Line,
            VoltageV3_3Line = req.VoltageV3_3Line,
            VoltageV5Line = req.VoltageV5Line,
            StandbyPowerSupply = req.StandbyPowerSupply,
            VoltageMinusV12 = req.VoltageMinusV12,
            InputVoltageRange = req.InputVoltageRange,
            CoolingType = req.CoolingType,
            FanDimensions = req.FanDimensions,
            Certificate = req.Certificate,
            PFC = req.PFC,
            ComplianceWithStandards = req.ComplianceWithStandards ?? Array.Empty<string>(),
            ProtectionTechnologies = req.ProtectionTechnologies ?? Array.Empty<string>(),
            Width = req.Width,
            Height = req.Height,
            Thickness = req.Thickness
        };

        _db.Psus.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("case")]
    public async Task<IActionResult> AddCase([FromBody] CaseCreateRequest req, CancellationToken ct)
    {
        var e = new Case
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            ManufacturerCode = req.ManufacturerCode,
            BodySize = req.BodySize,
            MotherBoardOrientation = req.MotherBoardOrientation,
            Length = req.Length,
            Width = req.Width,
            Height = req.Height,
            Weight = req.Weight,
            MainColor = req.MainColor,
            BodyMaterial = req.BodyMaterial ?? Array.Empty<string>(),
            SideWallWindow = req.SideWallWindow,
            WindowMaterial = req.WindowMaterial,
            FrontPanelMaterial = req.FrontPanelMaterial ?? Array.Empty<string>(),
            BackLightType = req.BackLightType,
            BackLightColor = req.BackLightColor,
            BackLightSource = req.BackLightSource,
            BackLightConnector = req.BackLightConnector,
            BackLightControl = req.BackLightControl ?? Array.Empty<string>(),
            CompatibleBoards = req.CompatibleBoards ?? Array.Empty<string>(),
            CompatiblePowerSupply = req.CompatiblePowerSupply ?? Array.Empty<string>(),
            PowerSupplyPlacement = req.PowerSupplyPlacement,
            PowerSupplyLength = req.PowerSupplyLength,
            HorizontalExpansionSlots = req.HorizontalExpansionSlots,
            GPULength = req.GPULength,
            MaxCoolerHeight = req.MaxCoolerHeight,
            Drives2_5 = req.Drives2_5,
            InternalCompartments3_5 = req.InternalCompartments3_5,
            ExternalCompartments3_5 = req.ExternalCompartments3_5,
            Drives5_25 = req.Drives5_25,
            IncludedFans = req.IncludedFans ?? Array.Empty<string>(),
            RearFanSupport = req.RearFanSupport ?? Array.Empty<string>(),
            TopFansSupport = req.TopFansSupport ?? Array.Empty<string>(),
            BottomFansSupport = req.BottomFansSupport ?? Array.Empty<string>(),
            SideFansSupport = req.SideFansSupport ?? Array.Empty<string>(),
            SZOSupport = req.SZOSupport,
            SZOUpperMountingDimension = req.SZOUpperMountingDimension ?? Array.Empty<string>(),
            SZORearMountingDimension = req.SZORearMountingDimension ?? Array.Empty<string>(),
            SZOSideMountingDimension = req.SZOSideMountingDimension ?? Array.Empty<string>()
        };

        _db.Cases.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("szo")]
    public async Task<IActionResult> AddSzo([FromBody] SzoCreateRequest req, CancellationToken ct)
    {
        var e = new Szo
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            MainColor = req.MainColor,
            BackLightType = req.BackLightType,
            BackLightSource = req.BackLightSource,
            BackLightConnector = req.BackLightConnector,
            LCD = req.LCD,
            Purpose = req.Purpose,
            WaterBlockMaterial = req.WaterBlockMaterial,
            WaterBlockSize = req.WaterBlockSize,
            Sockets = req.Sockets ?? Array.Empty<string>(),
            RadiatorMountingDimensions = req.RadiatorMountingDimensions,
            TDP = req.TDP,
            RadiatorLength = req.RadiatorLength,
            RadiatorWidth = req.RadiatorWidth,
            RadiatorThickness = req.RadiatorThickness,
            RadiatorMaterial = req.RadiatorMaterial,
            IncludedFans = req.IncludedFans,
            FanDimensions = req.FanDimensions,
            FanBearingType = req.FanBearingType,
            MinRotationSpeed = req.MinRotationSpeed,
            MaxRotationSpeed = req.MaxRotationSpeed,
            FanSpeedAdjustment = req.FanSpeedAdjustment,
            MaxNoiseLevel = req.MaxNoiseLevel,
            MaxAirFlow = req.MaxAirFlow,
            MaxStaticPressure = req.MaxStaticPressure,
            FanConnectionSocket = req.FanConnectionSocket,
            PumpNoiseLevel = req.PumpNoiseLevel,
            PumpRotationSpeed = req.PumpRotationSpeed,
            PumpConnectionSocket = req.PumpConnectionSocket,
            TubeLength = req.TubeLength
        };

        _db.Szos.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("aircooling")]
    public async Task<IActionResult> AddAirCooling([FromBody] AirCoolingCreateRequest req, CancellationToken ct)
    {
        var e = new AirCooling
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            ManufacturerCode = req.ManufacturerCode,
            Sockets = req.Sockets ?? Array.Empty<string>(),
            TDP = req.TDP,
            ConstructionType = req.ConstructionType,
            BaseMaterial = req.BaseMaterial,
            RadiatorMaterial = req.RadiatorMaterial,
            HeatPipes = req.HeatPipes,
            HeatPipeDiameter = req.HeatPipeDiameter,
            NickelPlatedCoating = req.NickelPlatedCoating,
            RadiatorColor = req.RadiatorColor,
            FansIncluded = req.FansIncluded,
            MaxFans = req.MaxFans,
            FansSize = req.FansSize,
            FanColor = req.FanColor,
            FanConnector = req.FanConnector,
            MinRotationSpeed = req.MinRotationSpeed,
            MaxRotationSpeed = req.MaxRotationSpeed,
            RotationAdjustment = req.RotationAdjustment,
            AirFlow = req.AirFlow,
            MaxStaticPressure = req.MaxStaticPressure,
            MaxNoiseLevel = req.MaxNoiseLevel,
            RatedCurrent = req.RatedCurrent,
            RatedVoltage = req.RatedVoltage,
            BearingType = req.BearingType,
            Height = req.Height,
            Length = req.Length,
            Width = req.Width,
            Weight = req.Weight,
            Additionally = req.Additionally ?? Array.Empty<string>()
        };

        _db.AirCoolings.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("memory")]
    public async Task<IActionResult> AddMemory([FromBody] MemoryCreateRequest req, CancellationToken ct)
    {
        var e = new Memory
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            ManufacturerCode = req.ManufacturerCode,
            MemoryType = req.MemoryType,
            MemoryModuleType = req.MemoryModuleType,
            TotalMemory = req.TotalMemory,
            OneModuleMemory = req.OneModuleMemory,
            TotalModules = req.TotalModules,
            RegisterMemory = req.RegisterMemory,
            ECCMemory = req.ECCMemory,
            Ranking = req.Ranking,
            ClockFrequency = req.ClockFrequency,
            AMDExpo = req.AMDExpo ?? Array.Empty<string>(),
            IntelXMP = req.IntelXMP ?? Array.Empty<string>(),
            CL = req.CL,
            TRCD = req.TRCD,
            TRP = req.TRP,
            Radiator = req.Radiator,
            RadiatorColor = req.RadiatorColor,
            BoardElementBacklight = req.BoardElementBacklight,
            Height = req.Height,
            LowProfile = req.LowProfile,
            Voltage = req.Voltage,
            Additionally = req.Additionally ?? Array.Empty<string>()
        };

        _db.Memories.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("ssd")]
    public async Task<IActionResult> AddSsd([FromBody] SsdCreateRequest req, CancellationToken ct)
    {
        var e = new Ssd
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            ManufacturerCode = req.ManufacturerCode,
            DiskCapacity = req.DiskCapacity,
            FormFactor = req.FormFactor,
            PhysInterface = req.PhysInterface,
            M2ConnectorKey = req.M2ConnectorKey,
            NVMe = req.NVMe,
            MemoryStructure = req.MemoryStructure,
            DRAM = req.DRAM,
            MaxReadSpeed = req.MaxReadSpeed,
            MaxWriteSpeed = req.MaxWriteSpeed,
            TBW = req.TBW,
            DWPD = req.DWPD,
            Radiator = req.Radiator,
            Length = req.Length,
            Width = req.Width,
            Thickness = req.Thickness,
            Weight = req.Weight
        };

        _db.Ssds.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("hdd2_5")]
    public async Task<IActionResult> AddHdd2_5([FromBody] Hdd2_5CreateRequest req, CancellationToken ct)
    {
        var e = new Hdd2_5
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            ManufacturerCode = req.ManufacturerCode,
            DiskCapacity = req.DiskCapacity,
            BufferSize = req.BufferSize,
            SpindleRotationSpeed = req.SpindleRotationSpeed,
            AVGAccessReading = req.AVGAccessReading,
            Interface = req.Interface,
            InterfaceBandwidth = req.InterfaceBandwidth,
            RecordingTechnology = req.RecordingTechnology,
            ImpactResistance = req.ImpactResistance,
            ParkingCycles = req.ParkingCycles,
            MinWorkingTemp = req.MinWorkingTemp,
            MaxWorkingTemp = req.MaxWorkingTemp,
            Width = req.Width,
            Length = req.Length,
            StandardThickness = req.StandardThickness,
            Weight = req.Weight
        };

        _db.Hdd2_5s.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }

    [HttpPost("hdd3_5")]
    public async Task<IActionResult> AddHdd3_5([FromBody] Hdd3_5CreateRequest req, CancellationToken ct)
    {
        var e = new Hdd3_5
        {
            Price = req.Price,
            Images = req.Images ?? Array.Empty<string>(),
            Warranty = req.Warranty,
            Model = req.Model,
            ManufacturerCode = req.ManufacturerCode,
            DiskCapacity = req.DiskCapacity,
            CacheSize = req.CacheSize,
            SpindleRotationSpeed = req.SpindleRotationSpeed,
            DataTransferRate = req.DataTransferRate,
            Interface = req.Interface,
            InterfaceBandwidth = req.InterfaceBandwidth,
            RAIDOptimization = req.RAIDOptimization,
            RecordingTechnology = req.RecordingTechnology,
            ImpactResistance = req.ImpactResistance,
            NoiseLevel = req.NoiseLevel,
            NoiseLevelIdle = req.NoiseLevelIdle,
            HeliumFilled = req.HeliumFilled,
            ParkingCycles = req.ParkingCycles,
            MaxPowerConsumption = req.MaxPowerConsumption,
            StandbyPowerConsumption = req.StandbyPowerConsumption,
            MaxWorkingTemp = req.MaxWorkingTemp,
            Width = req.Width,
            Length = req.Length,
            Thickness = req.Thickness,
            Weight = req.Weight
        };

        _db.Hdd3_5s.Add(e);
        await _db.SaveChangesAsync(ct);
        return Ok(new { id = e.Id });
    }
}