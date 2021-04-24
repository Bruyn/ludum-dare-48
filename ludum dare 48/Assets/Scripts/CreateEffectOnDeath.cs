using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class CreateEffectOnDeath : MonoBehaviour
{
    [SerializeField] private ParticleSystem sfxToStart;

    private Health health;

    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath.AddOnce(SpawnEffectOnDeath);
    }

    private void SpawnEffectOnDeath(Damage damage)
    {
        Instantiate(sfxToStart, transform.position, Quaternion.identity);
        //sfxToStart.Play();
    }
}
