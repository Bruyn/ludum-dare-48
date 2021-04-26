namespace BehaviorDesigner.Runtime.Tasks.Tutorials
{
    public class IsPunching : Action
    {
        private AiBehaviourTalker _talker;
        
        public override void OnAwake()
        {
            _talker = GetComponent<AiBehaviourTalker>();
        }
        
        public override TaskStatus OnUpdate()
        {
            if (_talker.IsPunching())
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}