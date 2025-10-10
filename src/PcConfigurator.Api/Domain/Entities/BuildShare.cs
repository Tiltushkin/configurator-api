namespace PcConfigurator.Api.Domain.Entities;

public class BuildShare
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid BuildId { get; set; }
    public Build Build { get; set; } = default!;
    public string Token { get; set; } = Guid.NewGuid().ToString("N");
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
}
