namespace IAM.GovernanceService.Models;

public class Policy
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // Custom DSL or C# script
    public bool IsActive { get; set; } = true;
}

public class EvaluationResult
{
    public bool IsAllowed { get; set; }
    public string? Reason { get; set; }
}
