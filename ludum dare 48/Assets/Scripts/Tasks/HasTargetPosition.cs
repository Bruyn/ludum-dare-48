using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Tasks
{
    public class HasTargetPosition : Decorator
    {
        public SharedVector3 targetPosition;
        
        public override bool CanExecute()
        {
            return !targetPosition.Value.Equals(Vector3.zero);
        }

        public override void OnChildExecuted(TaskStatus childStatus)
        {
            // Update the execution status after a child has finished running.
        }

        public override void OnEnd()
        {
        }
    }
}