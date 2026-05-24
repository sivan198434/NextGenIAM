using IAM.Shared.Abstractions;

namespace IAM.AuditService.Models;

public class AuditEntry : IAuditEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string AggregateId { get; set; } = string.Empty;
    public string EventType { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Payload { get; set; } = string.Empty;
    public string PreviousHash { get; set; } = string.Empty;
    public string Hash { get; set; } = string.Empty;
}
