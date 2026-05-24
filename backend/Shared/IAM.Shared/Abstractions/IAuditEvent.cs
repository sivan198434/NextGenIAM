namespace IAM.Shared.Abstractions;

public interface IAuditEvent
{
    string Id { get; }
    string AggregateId { get; }
    string EventType { get; }
    DateTime Timestamp { get; }
    string Payload { get; }
    string PreviousHash { get; set; }
    string Hash { get; set; }
}
