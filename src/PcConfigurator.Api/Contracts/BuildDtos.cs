using System;

namespace PcConfigurator.Api.Contracts
{
    public record BuildResponse(
        Guid Id,
        Guid OwnerId,
        string OwnerDisplayName,
        string Name,
        string? Description,
        Guid? CpuId,
        Guid? GpuId,
        Guid? MbId,
        Guid? PsuId,
        Guid? CaseId,
        Guid? CoolingId,
        Guid? MemoryId,
        Guid[]? SsdIds,
        Guid[]? HddIds,
        bool IsPublic,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );

    public record BuildCreateRequest(
        string? Name,
        string? Description,
        Guid? CpuId,
        Guid? GpuId,
        Guid? MbId,
        Guid? PsuId,
        Guid? CaseId,
        Guid? CoolingId,
        Guid? MemoryId,
        Guid[]? SsdIds,
        Guid[]? HddIds,
        bool IsPublic
    );

    public record BuildUpdateRequest(
        string? Name,
        string? Description,
        Guid? CpuId,
        Guid? GpuId,
        Guid? MbId,
        Guid? PsuId,
        Guid? CaseId,
        Guid? CoolingId,
        Guid? MemoryId,
        Guid[]? SsdIds,
        Guid[]? HddIds,
        bool IsPublic
    );

    public record ShareResponse(
        string Token,
        string Url,
        DateTime? ExpiresAt
    );
}