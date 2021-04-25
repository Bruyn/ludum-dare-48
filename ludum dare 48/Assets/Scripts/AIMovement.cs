using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public Animator Animator;

    private NavMeshAgent _navMeshAgent;

    public GameObject target;
    
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 screenPos = _camera.WorldToScreenPoint(transform.position);
        // Vector3 screenDir = (Input.mousePosition - screenPos).normalized;
        // Vector3 desiredDirection = forward * screenDir.y + right * screenDir.x;
        Vector3 desiredDirection = target.transform.position - transform.position;
        desiredDirection.Normalize();
        transform.rotation = Quaternion.LookRotation(desiredDirection);
        // direction = desiredDirection;
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