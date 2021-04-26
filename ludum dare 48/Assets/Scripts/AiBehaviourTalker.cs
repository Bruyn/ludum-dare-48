using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AiBehaviourTalker : MonoBehaviour
{
    public Animator Animator;

    [SerializeField] private GameObject _rightHand;
    [SerializeField] private float meleeAtackRadius = 0.5f;
    [SerializeField] private float meleeAtackDamageAmount = 10f;

    public CharAnimEventReceiver _animEventReceiver;

    private void Start()
    {
        _animEventReceiver.OnPunch.AddListener(Punch);
    }

    private void OnDestroy()
    {
        _animEventReceiver.OnPunch.RemoveListener(Punch);
    }

    public void ExecutePunch()
    {
        Animator.SetBool("isPunching", true);
    }

    void Punch(bool state)
    {
        Vector3 position = _rightHand.transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(position, meleeAtackRadius, LayerMask.GetMask("Player"));
        foreach (var hitCollider in hitColliders)
        {
            Damage damage = new Damage(gameObject, meleeAtackDamageAmount);
            hitCollider.gameObject.GetComponent<Health>().Damage(damage);
        }
    }

    public bool IsPunching()
    {
        return Animator.GetBool("isPunching");
    }
}