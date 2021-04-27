using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] private ParticleSystem sfxToStart;
    [SerializeField] private float damageAmount = 10f;
    [SerializeField] private float damageRadius = 3f;
    
    private Health _health;
    
    private void Start()
    {
        _health = GetComponent<Health>();
        _health.OnDeath.AddOnce(HandleOnDeath);
    }

    private void OnDestroy()
    {
        _health.OnDeath.RemoveOnce(HandleOnDeath);
    }

    private void HandleOnDeath(Damage deathDamage)
    {
        Vector3 position = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(position, damageRadius);
        foreach (var hitCollider in hitColliders)
        {
            Health health = hitCollider.gameObject.GetComponent<Health>();
            if (health != null)
            {
                Damage damage = new Damage(gameObject, damageAmount, DamageType.Barrel);
                health.Damage(damage);
            }
        }
        
        Instantiate(sfxToStart, position, Quaternion.identity);
        Destroy(gameObject);
    }
}