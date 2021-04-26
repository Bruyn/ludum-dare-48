using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Animator Animator;

    private NavMeshAgent _navMeshAgent;

    private Health _health;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_health.IsDead())
        {
            return;
        }
        
        SharedGameObject enemy = (SharedGameObject) GetComponent<BehaviorTree>().GetVariable("Enemy");
        if (enemy.Value == null)
        {
            return;
        }

        Vector3 desiredDirection = enemy.Value.transform.position - transform.position;
        desiredDirection.Normalize();
        transform.rotation = Quaternion.LookRotation(desiredDirection);
    }

    void FixedUpdate()
    {
        Vector3 movementVector = _navMeshAgent.velocity;
        movementVector.Normalize();

        Vector3 transformed = transform.InverseTransformVector(movementVector);
        Vector3 animDir = transformed.normalized;

        Animator.SetFloat("xAxis", animDir.x);
        Animator.SetFloat("yAxis", animDir.z);
    }
}