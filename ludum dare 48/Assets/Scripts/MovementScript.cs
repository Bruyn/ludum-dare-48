using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float Movementspeed = 5f;
    public Animator Animator;
    public LayerMask screenRayMask;

    private Rigidbody _rigidbody;
    private Vector3 _mousePosition;
    private Camera _camera;
    
    void Start()
    {
        _camera = Camera.main;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        float horAxis = Input.GetAxisRaw("Horizontal");
        float vertAxis = Input.GetAxisRaw("Vertical");
        
        Vector3 forward = _camera.transform.forward;
        Vector3 right = _camera.transform.right;
        
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
 
        Vector3 movementVector = forward * vertAxis + right * horAxis;
        movementVector.Normalize();
        _rigidbody.MovePosition(_rigidbody.transform.position + movementVector * (Movementspeed * Time.deltaTime));
        movement = movementVector;
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, 100f, screenRayMask.value);
        _mousePosition = hit.point;
        Vector3 lookingDirection = (_mousePosition - transform.position).normalized;
        lookingDirection.y = 0;        
        transform.rotation = Quaternion.LookRotation(lookingDirection);
        
        direction = lookingDirection;
        Vector3 transformed = transform.InverseTransformVector(movementVector);

        animDir = transformed;
        
        Animator.SetFloat("xAxis", transformed.x);
        Animator.SetFloat("yAxis", transformed.z);
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
