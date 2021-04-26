using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Tasks
{
    public class ClearTargetPosition : Action
    {
        public SharedVector3 targetPosition;
        
        public override TaskStatus OnUpdate()
        {
            targetPosition.Value = Vector3.zero;
            return TaskStatus.Success;
        }
    }
}