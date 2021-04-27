using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HasEnemy : Decorator
{
    public SharedGameObject Enemy;
    
    public override bool CanExecute()
    {
        if (Enemy.Value == null && gameObject.GetComponent<AIMovement>().isEnemyForced)
        {
            Enemy.Value = GameObject.FindGameObjectWithTag("Player");
        }
        
        return Enemy.Value != null && !Enemy.Value.GetComponent<Health>().IsDead();
    }

    public override void OnChildExecuted(TaskStatus childStatus)
    {
        // Update the execution status after a child has finished running.
    }

    public override void OnEnd()
    {
    }
}