using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(128)]
    public string Login { get; set; } = default!;

    [MaxLength(256)]
    public string PasswordHash { get; set; } = default!;

    [MaxLength(64)]
    public string DisplayName { get; set; } = "New User";

    public bool IsAdmin { get; set; } = false;

    [MaxLength(512)]
    public string AvatarUrl { get; set; } =
        "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_640.png";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
