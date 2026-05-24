using IAM.ConnectorService.Abstractions;
using IAM.Shared.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace IAM.ConnectorService.Consumers;

public class ProvisioningRequestedConsumer : IConsumer<ProvisioningRequestedEvent>
{
    private readonly IEnumerable<IConnector> _connectors;
    private readonly ILogger<ProvisioningRequestedConsumer> _logger;

    public ProvisioningRequestedConsumer(IEnumerable<IConnector> connectors, ILogger<ProvisioningRequestedConsumer> logger)
    {
        _connectors = connectors;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProvisioningRequestedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Processing provisioning request for {IdentityId} on {TargetSystem}", message.IdentityId, message.TargetSystem);

        var connector = _connectors.FirstOrDefault(c => c.SystemName == message.TargetSystem);
        if (connector == null)
        {
            _logger.LogError("Connector not found for {TargetSystem}", message.TargetSystem);
            await context.Publish(new ProvisioningCompletedEvent
            {
                CorrelationId = message.CorrelationId,
                Success = false,
                Error = $"Connector {message.TargetSystem} not found"
            });
            return;
        }

        try
        {
            switch (message.Action)
            {
                case "Create":
                    await connector.CreateAccountAsync(message.IdentityId, message.Attributes);
                    break;
                case "Update":
                    await connector.UpdateAccountAsync(message.IdentityId, message.Attributes);
                    break;
                case "Delete":
                    await connector.DeleteAccountAsync(message.IdentityId);
                    break;
            }

            await context.Publish(new ProvisioningCompletedEvent
            {
                CorrelationId = message.CorrelationId,
                Success = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Provisioning failed for {IdentityId}", message.IdentityId);
            await context.Publish(new ProvisioningCompletedEvent
            {
                CorrelationId = message.CorrelationId,
                Success = false,
                Error = ex.Message
            });
        }
    }
}
