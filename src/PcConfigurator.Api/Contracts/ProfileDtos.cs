using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Contracts;

public record ProfileResponse(Guid Id, string Login, string UserName, string AvatarUrl, DateTime CreatedAt);

public record ProfileUpdateRequest([Required, MaxLength(64)] string UserName, [Required, Url] string AvatarUrl);
