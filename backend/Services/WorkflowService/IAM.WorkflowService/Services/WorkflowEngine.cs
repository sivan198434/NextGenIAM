using IAM.WorkflowService.Models;

namespace IAM.WorkflowService.Services;

public class WorkflowEngine
{
    public async Task ExecuteAsync(WorkflowInstance instance, WorkflowDefinition definition)
    {
        var step = definition.Steps.FirstOrDefault(s => s.Id == (instance.CurrentStep ?? definition.Steps[0].Id));

        while (step != null)
        {
            instance.CurrentStep = step.Id;
            instance.Status = WorkflowStatus.InProgress;

            // Execute logic based on step.Action
            // This is where custom logic for "Approve", "Provision", etc would go.

            step = definition.Steps.FirstOrDefault(s => s.Id == step.NextStep);
        }

        instance.Status = WorkflowStatus.Completed;
    }
}
