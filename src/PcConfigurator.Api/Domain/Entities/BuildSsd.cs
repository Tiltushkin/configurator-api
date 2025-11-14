namespace PcConfigurator.Api.Domain.Entities;

public class BuildSsd
{
    public long Id { get; set; }
    public Guid BuildId { get; set; }
    public Build Build { get; set; } = default!;
    public Guid SsdId { get; set; }
}