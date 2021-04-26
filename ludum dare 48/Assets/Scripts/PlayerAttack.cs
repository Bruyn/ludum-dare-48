using System;
using System.Collections;
using System.Collections.Generic;
using Sigtrap.Relays;
using UnityEngine;

public class AttackStateChangedInfo
{
    public GameObject Target;
    public bool State;

    public AttackStateChangedInfo(GameObject target, bool state)
    {
        Target = target;
        State = state;
    }
}

public class PlayerAttack : MonoBehaviour
{
    public Relay<AttackStateChangedInfo> OnAttackStateChanged = new Relay<AttackStateChangedInfo>();
    
    public Gun _gun;
    
    [Header("AttackStats")]
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private float searchRadius = 2f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float timeScale = 0.5f;

    [Header("Debug")]
    [SerializeField] private bool debugEnabled = false;
    
    private Health attackTarget;
    private int attackCounter = 0;
    private bool isAttacking = false;
    private Vector3 jumpPosition;
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Enter()
    {
        attackTarget = GetNearestAttackObject();
        if (attackTarget == null)
            return;

        isAttacking = true;
        OnAttackStateChanged.Dispatch(new AttackStateChangedInfo(attackTarget.gameObject, isAttacking));
        rb.useGravity = false;
        Time.timeScale = timeScale;
        JumpToTarget();
    }

    private void Exit()
    {
        //Damage object
        attackTarget.Damage(new Damage(gameObject, damageAmount));

        isAttacking = false;
        rb.useGravity = true;
        Time.timeScale = 1;

        OnAttackStateChanged.Dispatch(new AttackStateChangedInfo(null, isAttacking));
    }

    private IEnumerator JumpCoroutine()
    {
        var playerPos = transform.position;
        playerPos.y = 0;
        float distance = Vector3.Distance(playerPos, jumpPosition);
        while (distance > 0.25f)
        {
            Vector3 direction = jumpPosition - transform.position;
            direction.y = transform.position.y < 1.8f ? 1 : 0;
            direction.Normalize();
            Vector3 posToMove = transform.position + direction * jumpSpeed * Time.fixedDeltaTime;
            rb.MovePosition(posToMove);

            playerPos = transform.position;
            playerPos.y = 0;
            jumpPosition.y = 0;

            distance = Vector3.Distance(playerPos, jumpPosition);

            if (debugEnabled)
            {
                Debug.DrawLine(transform.position, jumpPosition);
                Debug.Log("Distance: " + distance);
            }

            yield return null;
        }
        
        rb.MovePosition(jumpPosition);
        jumpPosition = Vector3.zero;

        Exit();
    }
    
    

    private void Update()
    {
        if (!isAttacking && Input.GetMouseButtonDown(1))
        {
            attackTarget = null;
            Enter();
        }
        
        if (Input.GetMouseButton(0))
        {
            _gun.Shoot();
        }
    }

    #region FindTarget
    //Finds Health objects closest to mouse cursor by it having collider
    //returns NULL on no target attack nearby
    private Health GetNearestAttackObject()
    {
        List<Collider> nearbyObjects = new List<Collider>();
        Vector3 mousePos = GetMousePosition();
        if (mousePos == Vector3.zero)
            return null;

        nearbyObjects.AddRange(Physics.OverlapSphere(mousePos, searchRadius));
        if (nearbyObjects.Count <= 0)
            return null;

        foreach (var nearbyObject in nearbyObjects)
        {
            attackTarget = nearbyObject.GetComponent<Health>();
            if (attackTarget != null)
                return attackTarget;
        }

        return null;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePosScreen = Input.mousePosition;
        Ray cameraRay = Camera.main.ScreenPointToRay(mousePosScreen);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(cameraRay, out var distance))
            return cameraRay.GetPoint(distance);

        return Vector3.zero;
    }
    #endregion

    #region AttackTarget

    private void JumpToTarget()
    {
        Vector3 dirToPlayer = transform.position - attackTarget.transform.position;
        dirToPlayer.y = 0;
        dirToPlayer.Normalize();
        Vector3 jumpPos = attackTarget.transform.position + dirToPlayer * attackDistance;
        jumpPosition = jumpPos;

        StartCoroutine(JumpCoroutine());
    }

    #endregion
}
