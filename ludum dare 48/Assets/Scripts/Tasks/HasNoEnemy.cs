using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class HasNoEnemy : Decorator
{
    public SharedGameObject Enemy;
    
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