namespace IAM.ConnectorService.Abstractions;

public interface IConnector
{
    string SystemName { get; }
    Task CreateAccountAsync(string identityId, Dictionary<string, string> attributes);
    Task UpdateAccountAsync(string identityId, Dictionary<string, string> attributes);
    Task DeleteAccountAsync(string identityId);
}
