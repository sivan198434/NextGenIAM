namespace IAM.Shared.Events;

public record ProvisioningRequestedEvent
{
    public Guid CorrelationId { get; init; }
    public string IdentityId { get; init; } = string.Empty;
    public string TargetSystem { get; init; } = string.Empty;
    public string Action { get; init; } = string.Empty; // Create, Update, Delete
    public Dictionary<string, string> Attributes { get; init; } = new();
}

public record ProvisioningCompletedEvent
{
    public Guid CorrelationId { get; init; }
    public bool Success { get; init; }
    public string? Error { get; init; }
}
