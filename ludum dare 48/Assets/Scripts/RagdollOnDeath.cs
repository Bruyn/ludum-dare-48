using System;
using UnityEngine;

public class RagdollOnDeath : MonoBehaviour
{
    [SerializeField] private Animator mainAnimator;
    [SerializeField] private GameObject gunToHide;
    
    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath.AddOnce(EnableRagdoll);
    }

    private void EnableRagdoll(Damage deathDamage)
    {
        mainAnimator.enabled = false;
        gunToHide.SetActive(false);
    }
}
