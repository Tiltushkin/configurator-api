using Microsoft.EntityFrameworkCore;
using PcConfigurator.Api.Domain.Entities;

namespace PcConfigurator.Api.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<User> Users => Set<User>();
    public DbSet<Cpu> Cpus => Set<Cpu>();
    public DbSet<Gpu> Gpus => Set<Gpu>();
    public DbSet<Mb> Mbs => Set<Mb>();
    public DbSet<Psu> Psus => Set<Psu>();
    public DbSet<Case> Cases => Set<Case>();
    public DbSet<Memory> Memories => Set<Memory>();
    public DbSet<Ssd> Ssds => Set<Ssd>();
    public DbSet<AirCooling> AirCoolings => Set<AirCooling>();
    public DbSet<Szo> Szos => Set<Szo>();
    public DbSet<Hdd2_5> Hdd2_5s => Set<Hdd2_5>();
    public DbSet<Hdd3_5> Hdd3_5s => Set<Hdd3_5>();
    public DbSet<Build> Builds => Set<Build>();
    public DbSet<BuildSsd> BuildSsds => Set<BuildSsd>();
    public DbSet<BuildHdd> BuildHdds => Set<BuildHdd>();
    public DbSet<BuildShare> BuildShares => Set<BuildShare>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Login)
            .IsUnique();

        modelBuilder.Entity<Cpu>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<Gpu>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<Mb>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<Psu>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<Case>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<Szo>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<AirCooling>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<Memory>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<Ssd>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<Hdd2_5>().Property(p => p.Price).HasPrecision(18,2);
        modelBuilder.Entity<Hdd3_5>().Property(p => p.Price).HasPrecision(18,2);

        modelBuilder.Entity<Cpu>().Property(p => p.Images).HasColumnType("text[]");
        modelBuilder.Entity<Cpu>().Property(p => p.MemoryTypes).HasColumnType("text[]");
        modelBuilder.Entity<Cpu>().Property(p => p.MemoryFrequency).HasColumnType("text[]");
        modelBuilder.Entity<Cpu>().Property(p => p.Additionally).HasColumnType("text[]");

        modelBuilder.Entity<Gpu>().Property(p => p.Images).HasColumnType("text[]");
        modelBuilder.Entity<Gpu>().Property(p => p.VideoConnectors).HasColumnType("text[]");
        modelBuilder.Entity<Gpu>().Property(p => p.Completion).HasColumnType("text[]");
        modelBuilder.Entity<Gpu>().Property(p => p.Additionally).HasColumnType("text[]");

        modelBuilder.Entity<Mb>().Property(p => p.Images).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.FormFactor).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.MemoryTypes).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.MemoryFormFactor).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.MaxMemoryBoostFrequency).HasColumnType("integer[]");
        modelBuilder.Entity<Mb>().Property(p => p.PCISlots).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.M2ConnectorsPCIeProcessor).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.M2ConnectorsPCIeCheapSet).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.SATARAID).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.NVMeRAID).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.VideoOutputs).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.ProcessorCoolingConnectors).HasColumnType("text[]");
        modelBuilder.Entity<Mb>().Property(p => p.PassiveCooling).HasColumnType("text[]");

        modelBuilder.Entity<Psu>().Property(p => p.Images).HasColumnType("text[]");
        modelBuilder.Entity<Psu>().Property(p => p.MainPowerConnector).HasColumnType("text[]");
        modelBuilder.Entity<Psu>().Property(p => p.ProcessorPowerConnectors).HasColumnType("text[]");
        modelBuilder.Entity<Psu>().Property(p => p.VideoCardPowerConnectors).HasColumnType("text[]");
        modelBuilder.Entity<Psu>().Property(p => p.ComplianceWithStandards).HasColumnType("text[]");
        modelBuilder.Entity<Psu>().Property(p => p.ProtectionTechnologies).HasColumnType("text[]");

        modelBuilder.Entity<Case>().Property(p => p.Images).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.BodyMaterial).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.FrontPanelMaterial).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.BackLightControl).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.CompatibleBoards).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.CompatiblePowerSupply).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.IncludedFans).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.RearFanSupport).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.TopFansSupport).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.BottomFansSupport).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.SideFansSupport).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.SZOUpperMountingDimension).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.SZORearMountingDimension).HasColumnType("text[]");
        modelBuilder.Entity<Case>().Property(p => p.SZOSideMountingDimension).HasColumnType("text[]");

        modelBuilder.Entity<Szo>().Property(p => p.Images).HasColumnType("text[]");
        modelBuilder.Entity<Szo>().Property(p => p.Sockets).HasColumnType("text[]");

        modelBuilder.Entity<AirCooling>().Property(p => p.Images).HasColumnType("text[]");
        modelBuilder.Entity<AirCooling>().Property(p => p.Sockets).HasColumnType("text[]");
        modelBuilder.Entity<AirCooling>().Property(p => p.Additionally).HasColumnType("text[]");

        modelBuilder.Entity<Memory>().Property(p => p.Images).HasColumnType("text[]");
        modelBuilder.Entity<Memory>().Property(p => p.AMDExpo).HasColumnType("text[]");
        modelBuilder.Entity<Memory>().Property(p => p.IntelXMP).HasColumnType("text[]");
        modelBuilder.Entity<Memory>().Property(p => p.Additionally).HasColumnType("text[]");
        
        modelBuilder.Entity<Ssd>().Property(p => p.Images).HasColumnType("text[]");

        modelBuilder.Entity<Hdd2_5>().Property(p => p.Images).HasColumnType("text[]");

        modelBuilder.Entity<Hdd3_5>().Property(p => p.Images).HasColumnType("text[]");

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Owner)
            .WithMany()
            .HasForeignKey(b => b.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Cpu)
            .WithMany()
            .HasForeignKey(b => b.CpuId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Gpu)
            .WithMany()
            .HasForeignKey(b => b.GpuId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Mb)
            .WithMany()
            .HasForeignKey(b => b.MbId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Psu)
            .WithMany()
            .HasForeignKey(b => b.PsuId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Case)
            .WithMany()
            .HasForeignKey(b => b.CaseId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Build>()
            .HasOne(b => b.Memory)
            .WithMany()
            .HasForeignKey(b => b.MemoryId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Build>()
            .HasMany(b => b.Ssds)
            .WithOne(bs => bs.Build)
            .HasForeignKey(bs => bs.BuildId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BuildSsd>(bs =>
        {
            bs.ToTable("BuildSsds");
            bs.HasKey(x => x.Id);
            bs.Property(x => x.Id).UseIdentityColumn();
            bs.HasIndex(x => new { x.BuildId });
            bs.HasOne(x => x.Build)
              .WithMany(b => b.Ssds)
              .HasForeignKey(x => x.BuildId)
              .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BuildHdd>()
            .HasKey(x => new { x.BuildId, x.DriveId });

        modelBuilder.Entity<BuildHdd>()
            .HasOne(x => x.Build)
            .WithMany(b => b.Hdds)
            .HasForeignKey(x => x.BuildId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BuildShare>()
            .HasIndex(s => s.Token)
            .IsUnique();
    }
}
