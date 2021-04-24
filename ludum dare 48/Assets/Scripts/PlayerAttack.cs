using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttack : MonoBehaviour
{
    [Header("AttackStats")]
    [SerializeField] private float damageAmount = 2f;
    [SerializeField] private float searchRadius = 2f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float knockbackForce = 10f;

    private Health attackTarget;
    private int attackCounter = 0;
    private bool isAttacking = false;
    private Vector3 jumpPosition;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isAttacking && Input.GetMouseButtonDown(0))
        {
            attackTarget = null;
            isAttacking = true;
            StartAttack();
        }
    }

    private void FixedUpdate()
    {
        if (jumpPosition != Vector3.zero)
        {
            Vector3 direction = jumpPosition - transform.position;
            direction.y = 0;
            direction.Normalize();
            Vector3 posToMove = transform.position + direction * jumpSpeed;
            rb.MovePosition(posToMove);
            //Reset if we arrived
            if (Vector3.Distance(transform.position, jumpPosition) < 0.5f)
            {
                rb.MovePosition(jumpPosition);
                jumpPosition = Vector3.zero;
                FinishAttack();
            }
        }
    }

    private void StartAttack()
    {
        attackTarget = GetNearestAttackObject();
        if (attackTarget == null)
        {
            isAttacking = false;
            return;
        }
        
        Debug.Log("Attacked target " + attackTarget + "!");
        JumpToTarget();
    }

    private void FinishAttack()
    {
        //Damage object
        attackTarget.Damage(new Damage(gameObject, damageAmount));

        //Knockback object
        var dirToEnemy = attackTarget.transform.position - transform.position;
        dirToEnemy.y = 0.2f;
        dirToEnemy.Normalize();
        
        attackTarget.GetComponent<Rigidbody>().AddForce(dirToEnemy * knockbackForce);
        
        isAttacking = false;
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
    }

    #endregion
}
