using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PcConfigurator.Api.Contracts;
using PcConfigurator.Api.Infrastructure;
using System.Text.Json;

namespace PcConfigurator.Api.Controllers;

[ApiController]
[Route("api/components")]
public class ComponentsController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IDistributedCache _cache;

    public ComponentsController(AppDbContext db, IDistributedCache cache)
    {
        _db = db;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> Smart([FromQuery] string type = "all", [FromQuery] string? search = null,
        [FromQuery] int limit = 50, [FromQuery] int offset = 0,
        [FromQuery] decimal? priceMin = null, [FromQuery] decimal? priceMax = null,
        [FromQuery] string? manufacturerCode = null, [FromQuery] string? model = null, [FromQuery] string? socket = null,
        CancellationToken ct = default)
    {
        type = type.ToLowerInvariant();
        var cacheKey = $"components:{type}:{search}:{limit}:{offset}:{priceMin}:{priceMax}:{manufacturerCode}:{model}:{socket}";
        var cached = await _cache.GetStringAsync(cacheKey, ct);
        if (cached is not null)
            return Content(cached, "application/json");

        var jsonObj = new Dictionary<string, object>();

        if (type is "cpu" or "all")
        {
            var q = _db.Cpus.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()) ||
                                 x.ManufacturerCode.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));
            if (!string.IsNullOrWhiteSpace(socket)) q = q.Where(x => x.Socket.ToLower().Contains(socket!.ToLower()));
            if (!string.IsNullOrWhiteSpace(manufacturerCode)) q = q.Where(x => x.ManufacturerCode.ToLower().Contains(manufacturerCode!.ToLower()));

            var rows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new CpuResponse(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.Socket, x.ManufacturerCode, x.ReleaseYear,
                    x.CoolingSystem, x.ThermalInterface, x.TotalCores, x.ProductiveCores, x.EnergyEfficientCores, x.Threads,
                    x.CacheL1, x.CacheL2, x.CacheL3, x.TechnicalProcess, x.Core, x.BasicFrequency, x.TurboFrequency,
                    x.EnergyEfficientBasicFrequency, x.EnergyEfficientTurboFrequency, x.FreeMultiplier, x.MemoryTypes,
                    x.MaxMemory, x.Canals, x.MemoryFrequency, x.ECCMode, x.TDP, x.BasicHeatProduction, x.MaxTemp,
                    x.GraphicalCore, x.PCIExpress, x.PCIExpressLines, x.Virtualization, x.Additionally
                )).ToListAsync(ct);

            jsonObj["cpus"] = rows;
        }

        if (type is "gpu" or "all")
        {
            var q = _db.Gpus.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()) ||
                                 x.ManufacturerCode.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));
            if (!string.IsNullOrWhiteSpace(manufacturerCode)) q = q.Where(x => x.ManufacturerCode.ToLower().Contains(manufacturerCode!.ToLower()));

            var rows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new GpuResponse(
                    x.Id, x.Price, x.Images, x.Warranty, x.Type, x.Model, x.ManufacturerCode, x.Color,
                    x.CanMining, x.LHR, x.GraphicalProcessor, x.MicroArch, x.TechProcess, x.BasicFrequency, x.TurboFrequency,
                    x.ALU, x.TextureBlocks, x.RasterizationBlocks, x.RayTracing, x.RtCores, x.TensorCores, x.VideoRAM,
                    x.MemoryType, x.MemoryBusWidth, x.MemoryBandwidth, x.MemoryFrequency, x.VideoConnectors, x.HDMIVersion,
                    x.DisplayPortVersion, x.MaximumMonitors, x.MaximumResolution, x.ConnectionInterface, x.ConnectionFormFactor,
                    x.PCIExpressLines, x.PowerConnections, x.RecommendedPowerSupply, x.PowerConsumption, x.CoolingType, x.NumberOfFans,
                    x.LiquidCoolingRadiator, x.Backlight, x.RGBSync, x.LCD, x.BiosToggler, x.Completion, x.Additionally,
                    x.LowProfile, x.ExpansionSlots, x.Width, x.Height, x.Thickness, x.Weight
                )).ToListAsync(ct);

            jsonObj["gpus"] = rows;
        }

        if (type is "mb" or "all")
        {
            var q = _db.Mbs.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));

            var mbRows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new MbResponse(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.Series, x.Color, x.ReleaseYear,
                    x.FormFactor, x.Width, x.Height, x.Socket, x.CheapSet, x.MemoryTypes, x.MaxMemory,
                    x.MemoryFormFactor, x.MemorySlots, x.MemoryCanals, x.MaxMemoryFrequency,
                    x.MaxMemoryBoostFrequency, x.PCIExpress, x.PCISlots, x.SLI, x.CardsInSLI, x.PCIEx1Slots,
                    x.NVMeSupport, x.DiskPCIExpress, x.M2Slots, x.M2ConnectorsPCIeProcessor,
                    x.M2ConnectorsPCIeCheapSet, x.SATAPorts, x.SATARAID, x.NVMeRAID, x.OutUSBTypeA, x.OutUSBTypeC, x.InUSBTypeA, x.InUSBTypeC,
                    x.VideoOutputs, x.NetworkPorts, x.ProcessorCoolingConnectors, x.SZOConnectors,
                    x.CaseFansConnectors4Pin, x.CaseFansConnectors3Pin, x.VDGConnectors, x.VGRBConnectors,
                    x.M2Wireless, x.RS232, x.LPT, x.MainPowerConnector, x.ProcessorPowerConnector,
                    x.PowerPhases, x.PassiveCooling
                )).ToListAsync(ct);

            jsonObj["mbs"] = mbRows;
        }

        if (type is "psu" or "all")
        {
            var q = _db.Psus.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));

            var psuRows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new PsuResponse(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.Power, x.FormFactor, x.Color,
                    x.DetachableCables, x.CablesColor, x.BacklightType, x.MainPowerConnector,
                    x.ProcessorPowerConnectors, x.Floppy4PinConnector, x.VideoCardPowerConnectors,
                    x.SATAConnectors, x.MolexConnectors, x.MainPowerCableLength, x.ProcessorPowerCableLength,
                    x.PCIEPowerCableLength, x.SATAPowerCableLength, x.MolexPowerCableLength, x.PowerV12Line,
                    x.VoltageV12Line, x.VoltageV3_3Line, x.VoltageV5Line, x.StandbyPowerSupply, x.VoltageMinusV12,
                    x.InputVoltageRange, x.CoolingType, x.FanDimensions, x.Certificate, x.PFC,
                    x.ComplianceWithStandards, x.ProtectionTechnologies, x.Width, x.Height, x.Thickness
                )).ToListAsync(ct);

            jsonObj["psus"] = psuRows;
        }

        if (type is "case" or "all")
        {
            var q = _db.Cases.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()) ||
                                x.ManufacturerCode.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));
            if (!string.IsNullOrWhiteSpace(manufacturerCode)) q = q.Where(x => x.ManufacturerCode.ToLower().Contains(manufacturerCode!.ToLower()));

            var caseRows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new CaseResponse(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.ManufacturerCode, x.BodySize, x.MotherBoardOrientation,
                    x.Length, x.Width, x.Height, x.Weight, x.MainColor, x.BodyMaterial, x.SideWallWindow, x.WindowMaterial,
                    x.FrontPanelMaterial, x.BackLightType, x.BackLightColor, x.BackLightSource, x.BackLightConnector, x.BackLightControl,
                    x.CompatibleBoards, x.CompatiblePowerSupply, x.PowerSupplyPlacement, x.PowerSupplyLength, x.HorizontalExpansionSlots,
                    x.GPULength, x.MaxCoolerHeight, x.Drives2_5, x.InternalCompartments3_5, x.ExternalCompartments3_5, x.Drives5_25,
                    x.IncludedFans, x.RearFanSupport, x.TopFansSupport, x.BottomFansSupport, x.SideFansSupport, x.SZOSupport, x.SZOUpperMountingDimension,
                    x.SZORearMountingDimension, x.SZOSideMountingDimension
                )).ToListAsync(ct);

            jsonObj["cases"] = caseRows;
        }

        if (type is "szo" or "all")
        {
            var q = _db.Szos.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));

            var szoRows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new SzoResponse(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.MainColor, x.BackLightType, x.BackLightSource,
                    x.BackLightConnector, x.LCD, x.Purpose, x.WaterBlockMaterial, x.WaterBlockSize, x.Sockets,
                    x.RadiatorMountingDimensions, x.TDP, x.RadiatorLength, x.RadiatorWidth, x.RadiatorThickness,
                    x.RadiatorMaterial, x.IncludedFans, x.FanDimensions, x.FanBearingType, x.MinRotationSpeed,
                    x.MaxRotationSpeed, x.FanSpeedAdjustment, x.MaxNoiseLevel, x.MaxAirFlow, x.MaxStaticPressure,
                    x.FanConnectionSocket, x.PumpNoiseLevel, x.PumpRotationSpeed, x.PumpConnectionSocket, x.TubeLength
                )).ToListAsync(ct);

            jsonObj["szos"] = szoRows;
        }

        if (type is "aircooling" or "all")
        {
            var q = _db.AirCoolings.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()) ||
                                    x.ManufacturerCode.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));
            if (!string.IsNullOrWhiteSpace(manufacturerCode)) q = q.Where(x => x.ManufacturerCode.ToLower().Contains(manufacturerCode!.ToLower()));

            var airCoolingRows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new AirCoolingResponse(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.ManufacturerCode, x.Sockets, x.TDP, x.ConstructionType,
                    x.BaseMaterial, x.RadiatorMaterial, x.HeatPipes, x.HeatPipeDiameter, x.NickelPlatedCoating, x.RadiatorColor,
                    x.FansIncluded, x.MaxFans, x.FansSize, x.FanColor, x.FanConnector, x.MinRotationSpeed, x.MaxRotationSpeed,
                    x.RotationAdjustment, x.AirFlow, x.MaxStaticPressure, x.MaxNoiseLevel, x.RatedCurrent, x.RatedVoltage, x.BearingType,
                    x.Height, x.Length, x.Width, x.Weight, x.Additionally
                )).ToListAsync(ct);

            jsonObj["airCoolings"] = airCoolingRows;
        }

        if (type is "memory" or "all")
        {
            var q = _db.Memories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()) ||
                                    x.ManufacturerCode.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));
            if (!string.IsNullOrWhiteSpace(manufacturerCode)) q = q.Where(x => x.ManufacturerCode.ToLower().Contains(manufacturerCode!.ToLower()));

            var memoriesRows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new MemoryResponse(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.ManufacturerCode, x.MemoryType, x.MemoryModuleType,
                    x.TotalMemory, x.OneModuleMemory, x.TotalModules, x.RegisterMemory, x.ECCMemory, x.Ranking, x.ClockFrequency,
                    x.AMDExpo, x.IntelXMP, x.CL, x.TRCD, x.TRP, x.Radiator, x.RadiatorColor, x.BoardElementBacklight, x.Height,
                    x.LowProfile, x.Voltage, x.Additionally
                )).ToListAsync(ct);

            jsonObj["memories"] = memoriesRows;
        }

        if (type is "ssd" or "all")
        {
            var q = _db.Ssds.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()) ||
                                    x.ManufacturerCode.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));
            if (!string.IsNullOrWhiteSpace(manufacturerCode)) q = q.Where(x => x.ManufacturerCode.ToLower().Contains(manufacturerCode!.ToLower()));

            var ssdRows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new SsdResponse(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.ManufacturerCode, x.DiskCapacity, x.FormFactor,
                    x.PhysInterface, x.M2ConnectorKey, x.NVMe, x.MemoryStructure, x.DRAM, x.MaxReadSpeed, x.MaxWriteSpeed,
                    x.TBW, x.DWPD, x.Radiator, x.Length, x.Width, x.Thickness, x.Weight
                )).ToListAsync(ct);

            jsonObj["ssds"] = ssdRows;
        }

        if (type is "hdd2_5" or "all")
        {
            var q = _db.Hdd2_5s.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()) ||
                                    x.ManufacturerCode.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));
            if (!string.IsNullOrWhiteSpace(manufacturerCode)) q = q.Where(x => x.ManufacturerCode.ToLower().Contains(manufacturerCode!.ToLower()));

            var hdd2_5Rows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new Hdd2_5Response(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.ManufacturerCode, x.DiskCapacity, x.BufferSize,
                    x.SpindleRotationSpeed, x.AVGAccessReading, x.Interface, x.InterfaceBandwidth, x.RecordingTechnology,
                    x.ImpactResistance, x.ParkingCycles, x.MinWorkingTemp, x.MaxWorkingTemp, x.Width, x.Length,
                    x.StandardThickness, x.Weight
                )).ToListAsync(ct);

            jsonObj["hdd2_5"] = hdd2_5Rows;
        }
            
        if (type is "hdd3_5" or "all")
        {
            var q = _db.Hdd3_5s.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(x => x.Model.ToLower().Contains(search!.ToLower()) ||
                                x.ManufacturerCode.ToLower().Contains(search!.ToLower()));
            if (priceMin.HasValue) q = q.Where(x => x.Price >= priceMin.Value);
            if (priceMax.HasValue) q = q.Where(x => x.Price <= priceMax.Value);
            if (!string.IsNullOrWhiteSpace(model)) q = q.Where(x => x.Model.ToLower().Contains(model!.ToLower()));
            if (!string.IsNullOrWhiteSpace(manufacturerCode)) q = q.Where(x => x.ManufacturerCode.ToLower().Contains(manufacturerCode!.ToLower()));

            var hdd3_5Rows = await q.OrderBy(x => x.Price)
                .Skip(offset).Take(Math.Min(limit, 200))
                .Select(x => new Hdd3_5Response(
                    x.Id, x.Price, x.Images, x.Warranty, x.Model, x.ManufacturerCode, x.DiskCapacity, x.CacheSize,
                    x.SpindleRotationSpeed, x.DataTransferRate, x.Interface, x.InterfaceBandwidth, x.RAIDOptimization,
                    x.RecordingTechnology, x.ImpactResistance, x.NoiseLevel, x.NoiseLevelIdle, x.HeliumFilled, x.ParkingCycles,
                    x.MaxPowerConsumption, x.StandbyPowerConsumption, x.MaxWorkingTemp, x.Width, x.Length, x.Thickness, x.Weight
                )).ToListAsync(ct);

            jsonObj["hdd3_5"] = hdd3_5Rows;
        }

        var json = JsonSerializer.Serialize(jsonObj);
        var opts = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
        };
        await _cache.SetStringAsync(cacheKey, json, opts, ct);
        return Content(json, "application/json");
    }
}
