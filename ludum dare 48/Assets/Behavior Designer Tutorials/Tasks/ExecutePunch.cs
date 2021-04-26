using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Tutorials
{
    public class ExecutePunch : Action
    {
        private AiBehaviourTalker _talker;
        
        public override void OnAwake()
        {
            _talker = GetComponent<AiBehaviourTalker>();
        }
        
        public override TaskStatus OnUpdate()
        {
            _talker.ExecutePunch();
            return TaskStatus.Success;
        }
    }
}