using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HasNoEnemy : Decorator
{
    public SharedGameObject Enemy;
    
    // The status of the child after it has finished running.
    private TaskStatus executionStatus = TaskStatus.Inactive;

    public override bool CanExecute()
    {
        return Enemy.Value == null;
    }

    public override void OnChildExecuted(TaskStatus childStatus)
    {
        // Update the execution status after a child has finished running.
    }

    public override void OnEnd()
    {
    }
}