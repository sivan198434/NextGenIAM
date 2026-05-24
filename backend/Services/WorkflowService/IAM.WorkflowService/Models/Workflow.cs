namespace IAM.WorkflowService.Models;

public enum WorkflowStatus
{
    Pending,
    InProgress,
    Completed,
    Failed
}

public class WorkflowInstance
{
    public Guid Id { get; set; }
    public string WorkflowType { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public WorkflowStatus Status { get; set; } = WorkflowStatus.Pending;
    public string? CurrentStep { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class WorkflowDefinition
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<WorkflowStep> Steps { get; set; } = new();
}

public class WorkflowStep
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string? NextStep { get; set; }
}
