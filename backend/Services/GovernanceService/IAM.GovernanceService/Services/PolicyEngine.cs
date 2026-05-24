using IAM.GovernanceService.Models;

namespace IAM.GovernanceService.Services;

public class PolicyEngine
{
    public EvaluationResult Evaluate(Policy policy, Dictionary<string, object> context)
    {
        // Simple mock of a custom engine.
        // In a real implementation, this might use Roslyn for script execution
        // or a custom parser for a domain-specific language.

        if (policy.Code.Contains("MustHaveEmail") && !context.ContainsKey("Email"))
        {
            return new EvaluationResult { IsAllowed = false, Reason = "Missing Email" };
        }

        return new EvaluationResult { IsAllowed = true };
    }
}
