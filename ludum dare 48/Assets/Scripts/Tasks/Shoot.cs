using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskCategory("USSR")]
public class Shoot : Action
{
    [UnityEngine.Tooltip("The speed of the agent")]
    public SharedFloat speed = 10;

    [UnityEngine.Tooltip("The angular speed of the agent")]
    public SharedFloat angularSpeed = 120;

    [UnityEngine.Tooltip(
        "The agent has arrived when the destination is less than the specified amount. This distance should be greater than or equal to the NavMeshAgent StoppingDistance.")]
    public SharedFloat arriveDistance = 0.2f;

    [UnityEngine.Tooltip("The GameObject that the agent is seeking")]
    public SharedGameObject target;

    [UnityEngine.Tooltip("If target is null then use the target position")]
    public SharedVector3 targetPosition;

    // Component references
    private Gun _gun;

    /// <summary>
    /// Cache the component references.
    /// </summary>
    public override void OnAwake()
    {
        _gun = gameObject.GetComponentInChildren<Gun>();
    }

    /// <summary>
    /// Allow pathfinding to resume.
    /// </summary>
    public override void OnStart()
    {
    }

    // Seek the destination. Return success once the agent has reached the destination.
    // Return running if the agent hasn't reached the destination yet
    public override TaskStatus OnUpdate()
    {
        _gun.Shoot();
        return TaskStatus.Success;

        //return TaskStatus.Running;
    }


    /// <summary>
    /// The task has ended. Stop moving.
    /// </summary>
    public override void OnEnd()
    {
    }

    /// <summary>
    /// The behavior tree has ended. Stop moving.
    /// </summary>
    public override void OnBehaviorComplete()
    {
    }
}