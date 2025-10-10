using System.ComponentModel.DataAnnotations;

namespace PcConfigurator.Api.Contracts;

public record RegisterRequest(
    [Required] string Login,
    [Required, MinLength(6)] string Password,
    string? DisplayName
);

public record LoginRequest(
    [Required] string Login,
    [Required] string Password
);

public record TokenResponse(string AccessToken, DateTime ExpiresAtUtc);
