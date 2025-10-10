namespace PcConfigurator.Api.Domain.Entities;

public class BuildHdd
{
    public Guid BuildId { get; set; }
    public Guid DriveId { get; set; }
    public HddKind DriveKind { get; set; }

    public Build Build { get; set; } = default!;
}