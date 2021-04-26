using System;
using System.Collections.Generic;
using UnityEngine;

public class RagdollOnDeath : MonoBehaviour
{
    [SerializeField] private Animator mainAnimator;
    [SerializeField] private GameObject gunToHide;
    [SerializeField] private Collider mainCollider;
    [SerializeField] private Rigidbody mainRigidbody;

    private Health health;
    private List<Collider> allColliders = new List<Collider>();
    private List<Rigidbody> allRigidbodies = new List<Rigidbody>();

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath.AddOnce(EnableRagdoll);

        if (mainCollider != null)
        {
            allColliders.AddRange(GetComponentsInChildren<Collider>());
            foreach (var collider in allColliders)
            {
                collider.enabled = false;
            }

            mainCollider.enabled = true;
        }

        if (mainRigidbody != null)
        {
            allRigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
            foreach (var rigidbody in allRigidbodies)
            {
                rigidbody.isKinematic = true;
            }

            mainRigidbody.isKinematic = false;
        }
    }

    private void EnableRagdoll(Damage deathDamage)
    {
        mainAnimator.enabled = false;
        gunToHide.SetActive(false);

        if (mainCollider != null)
        {
            foreach (var collider in allColliders)
            {
                collider.enabled = true;
            }
        }

        if (mainRigidbody != null)
        {
            foreach (var rigidbody in allRigidbodies)
            {
                rigidbody.isKinematic = false;
            }
            mainRigidbody.isKinematic = true;
        }
    }
}
