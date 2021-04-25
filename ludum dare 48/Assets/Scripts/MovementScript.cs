using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float Movementspeed = 5f;
    public Animator Animator;
    public CharAnimEventReceiver _animEventReceiver;

    private Rigidbody _rigidbody;
    private Vector3 _mousePosition;
    private Camera _camera;

    private PlayerAttack _playerAttack;

    private bool isKicking = false;
    private bool isKickLanded = true;
    
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        
        _playerAttack = GetComponent<PlayerAttack>();
        _playerAttack.OnAttackStateChanged.AddListener(AttackStateChanged);

        _animEventReceiver.KickLanded.AddListener(KickLanded);
    }

    void AttackStateChanged(AttackStateChangedInfo state)
    {
        Animator.SetBool("isKicking", state.State);
        isKicking = state.State;
        
        if (isKicking)
        {
            Vector3 dirToTarget = state.Target.transform.position - transform.position;
            dirToTarget.Normalize();
            dirToTarget.y = 0;

            Quaternion rotation = Quaternion.LookRotation(dirToTarget);
            transform.rotation = rotation;
            transform.Rotate(Vector3.up, 90);
        }
        else
        {
            isKickLanded = false;
            Debug.Log("Not attacking");
        }
    }

    void KickLanded(bool state)
    {
        isKickLanded = true;
        Debug.Log("Landed");
    }

    void FixedUpdate()
    {
        if (isKicking || !isKickLanded)
            return;
        
        float horAxis = Input.GetAxisRaw("Horizontal");
        float vertAxis = Input.GetAxisRaw("Vertical");
        
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;
        
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        
        Vector3 movementVector = forward * vertAxis + right * horAxis;

        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5))
        {
            movementVector = Vector3.ProjectOnPlane(movementVector, hit.normal);
        }
        movementVector.Normalize();
        
        movementVector *= Movementspeed;
        movementVector.y = _rigidbody.velocity.y;
        _rigidbody.velocity = movementVector;
        
        movement = movementVector;
        
        Vector3 screenPos = _camera.WorldToScreenPoint(transform.position);
        Vector3 screenDir = (Input.mousePosition - screenPos).normalized;
        Vector3 desiredDirection = forward * screenDir.y + right * screenDir.x;
        transform.rotation = Quaternion.LookRotation(desiredDirection);
        direction = desiredDirection;
        
        Vector3 transformed = transform.InverseTransformVector(movementVector);
        animDir = transformed.normalized;
        
        Animator.SetFloat("xAxis", animDir.x);
        Animator.SetFloat("yAxis", animDir.z);
    }

    private Vector3 direction;
    private Vector3 movement;
    private Vector3 animDir;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + direction);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + movement);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + animDir);

    }
}
