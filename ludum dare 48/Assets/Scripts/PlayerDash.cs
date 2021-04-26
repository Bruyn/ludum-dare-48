using BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashTime = .3f;
    [SerializeField] private float dashCooldown = 1f;

    private MovementScript playerMovement;
    private Rigidbody rb;
    private float currentTime;
    private float currentCooldown;
    private bool isDashing;
    private Vector3 dashDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<MovementScript>();
    }

    private void Update()
    {
        if (isDashing)
        {
            CheckCollision();
            Dash();
            return;
        }

        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <= 0)
                HudController.Instance.UpdateDashStatus(true);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashDirection = playerMovement.GetMovementVector().normalized;
            dashDirection.y = 0;
            isDashing = true;
        }
    }

    private void Dash()
    {
        rb.MovePosition(transform.position + dashDirection * dashSpeed * Time.deltaTime);
        currentTime += Time.deltaTime;
        if (currentTime >= dashTime)
            StopDash();
    }

    private void CheckCollision()
    {
        Ray ray = new Ray(transform.position + Vector3.up, dashDirection);
        if (Physics.Raycast(ray, dashSpeed * Time.deltaTime))
        {
            StopDash();
        }
    }

    private void StopDash()
    {
        isDashing = false;
        currentCooldown = dashCooldown;
        currentTime = 0;
        HudController.Instance.UpdateDashStatus(false);
    }
}
