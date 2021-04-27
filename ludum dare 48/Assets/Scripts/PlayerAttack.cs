using System;
using System.Collections;
using System.Collections.Generic;
using Sigtrap.Relays;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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
    public CharAnimEventReceiver _animEventReceiver;

    [Header("AttackStats")] [SerializeField]
    private float damageAmount = 2f;

    [SerializeField] private float searchRadius = 2f;
    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float jumpSpeed = 3f;
    [SerializeField] private float maxKickDistance = 5f;
    [SerializeField] private float timeScale = 0.5f;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _gunGameObject;
    [SerializeField] private Rig _rig;


    [SerializeField] private GameObject _rightHand;
    [SerializeField] public bool meleeAtack = true;
    [SerializeField] private bool isKickEnabled = false;
    [SerializeField] private float meleeAtackRadius = 0.5f;
    [SerializeField] private float meleeAtackDamageAmount = 10f;

    [Header("Debug")] [SerializeField] private bool debugEnabled = false;

    private Gun _gun;
    private Health attackTarget;
    private int attackCounter = 0;
    private bool isAttacking = false;
    private Vector3 jumpPosition;
    private Rigidbody rb;

    private Health _health;

    public bool IsKicking { get; private set; } = false;
    public bool IsKickLanded { get; private set; } = true;

    public void EnableKick()
    {
        isKickEnabled = true;
    }

    public void EnableGun()
    {
        meleeAtack = false;
        _rig.weight = 1;
        _gunGameObject.SetActive(true);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _animEventReceiver.KickLanded.AddListener(KickLanded);
        _animEventReceiver.OnPunch.AddListener(Punch);
        _gun = _gunGameObject.GetComponent<Gun>();
        _health = GetComponent<Health>();

        if (meleeAtack)
        {
            _rig.weight = 0;
            _gunGameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _animEventReceiver.KickLanded.RemoveListener(KickLanded);
        _animEventReceiver.OnPunch.RemoveListener(Punch);
    }

    void KickLanded(bool state)
    {
        IsKickLanded = true;
    }
    
    void Punch(bool state)
    {
        Vector3 position = _rightHand.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(position, meleeAtackRadius, LayerMask.GetMask("AI"));
        foreach (var hitCollider in hitColliders)
        {
            Damage damage = new Damage(gameObject, meleeAtackDamageAmount, DamageType.Fist);
            hitCollider.gameObject.GetComponent<Health>().Damage(damage);
        }
    }

    private void Enter()
    {
        attackTarget = GetNearestAttackObject();
        if (attackTarget == null)
            return;

        isAttacking = true;
        AttackStateChanged(new AttackStateChangedInfo(attackTarget.gameObject, isAttacking));

        rb.useGravity = false;
        SoundManager.Instance.PlayKarateSound();
        JumpToTarget();
    }

    private void Exit()
    {
        //Damage object
        attackTarget.Damage(new Damage(gameObject, damageAmount, DamageType.Kick));

        isAttacking = false;
        rb.useGravity = true;
        AttackStateChanged(new AttackStateChangedInfo(null, isAttacking));
    }

    void AttackStateChanged(AttackStateChangedInfo state)
    {
        _animator.SetBool("isKicking", state.State);
        IsKicking = state.State;
        HudController.Instance.UpdateKickStatus(!state.State);

        if (IsKicking)
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
            IsKickLanded = false;
        }
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
        if (_health.IsDead())
        {
            return;
        }

        if (isKickEnabled && !isAttacking && Input.GetMouseButtonDown(1))
        {
            attackTarget = null;
            Enter();
        }

        if (Input.GetMouseButton(0))
        {
            if (meleeAtack)
            {
                ExecutePunch();
            }
            else
            {
                _gun.Shoot();
            }
        }
    }

    public void ExecutePunch()
    {
        if (!IsPunching())
        {
            SoundManager.Instance.PlayWooshSound();
        }

        _animator.SetBool("isPunching", true);
    }

    public bool IsPunching()
    {
        return _animator.GetBool("isPunching");
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

        Vector3 playerPos = transform.position;
        
        foreach (var nearbyObject in nearbyObjects)
        {
            var newAttackTarget = nearbyObject.GetComponent<Health>();
            if (newAttackTarget == null)
                continue;
            if (newAttackTarget.IsDead() || newAttackTarget.gameObject == gameObject)
                continue;

            Vector3 targetPos = newAttackTarget.transform.position;
            float distance = Vector3.Distance(playerPos, targetPos);
            if (distance > maxKickDistance)
                continue;
            
            Vector3 direction = (targetPos - playerPos).normalized;
            Ray ray = new Ray(playerPos, direction);
            RaycastHit []hits = Physics.RaycastAll(ray, distance);
            bool mustSkip = false;
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject != gameObject && hit.collider.gameObject != newAttackTarget.gameObject)
                {
                    mustSkip = true;
                    break;
                }
            }

            if (mustSkip)
            {
                continue;
            }
            
            return newAttackTarget;
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