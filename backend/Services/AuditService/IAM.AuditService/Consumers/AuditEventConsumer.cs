using IAM.AuditService.Services;
using IAM.Shared.Abstractions;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IAM.AuditService.Consumers;

public class AuditEventConsumer : IConsumer<IAuditEvent>
{
    private readonly AuditStore _auditStore;
    private readonly ILogger<AuditEventConsumer> _logger;

    public AuditEventConsumer(AuditStore auditStore, ILogger<AuditEventConsumer> logger)
    {
        _auditStore = auditStore;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<IAuditEvent> context)
    {
        var @event = context.Message;
        _logger.LogInformation("Processing audit event: {EventType} for Aggregate: {AggregateId}", @event.EventType, @event.AggregateId);
        await _auditStore.AppendAsync(@event.AggregateId, @event.EventType, @event.Payload);
    }
}
