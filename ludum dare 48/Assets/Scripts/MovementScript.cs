using BehaviorDesigner.Runtime.Tasks.Unity.Math;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float Movementspeed = 5f;
    public Animator Animator;

    private Rigidbody _rigidbody;
    private Vector3 _mousePosition;
    private Camera _camera;

    private Health _health;
    private PlayerAttack _playerAttack;

    public bool isGameplayStarted;
    
    public void StartGameplay()
    {
        isGameplayStarted = true;
    }

    public void StartSmoking()
    {
        Animator.SetTrigger("startSmoking");
    }
    
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody>();
        _playerAttack = GetComponent<PlayerAttack>();
        _health = GetComponent<Health>();
    }

    void FixedUpdate()
    {
        if (_health.IsDead())
        {
            return;
        }

        if (!isGameplayStarted || _playerAttack.IsKicking || !_playerAttack.IsKickLanded)
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

    public Vector3 GetMovementVector()
    {
        return movement;
    }

}