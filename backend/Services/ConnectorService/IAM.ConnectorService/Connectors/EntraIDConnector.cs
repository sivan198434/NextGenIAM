using IAM.ConnectorService.Abstractions;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;

namespace IAM.ConnectorService.Connectors;

public class EntraIDConnector : IConnector
{
    private readonly ILogger<EntraIDConnector> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;

    public EntraIDConnector(ILogger<EntraIDConnector> logger)
    {
        _logger = logger;
        SystemName = "EntraID";
        _retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    public string SystemName { get; }

    public async Task CreateAccountAsync(string identityId, Dictionary<string, string> attributes)
    {
        await _retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Creating account in Entra ID for {IdentityId}", identityId);
            // Simulate API call
            await Task.Delay(100);
        });
    }

    public async Task UpdateAccountAsync(string identityId, Dictionary<string, string> attributes)
    {
        await _retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Updating account in Entra ID for {IdentityId}", identityId);
            await Task.Delay(100);
        });
    }

    public async Task DeleteAccountAsync(string identityId)
    {
        await _retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Deleting account in Entra ID for {IdentityId}", identityId);
            await Task.Delay(100);
        });
    }
}
