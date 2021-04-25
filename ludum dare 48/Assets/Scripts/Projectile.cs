using System;
using UnityEngine;

public class Projectile : PooledBehavior
{
    
    [SerializeField]
    private float speed = 3f;
    private Damage damage;
    private Vector3 startPos = Vector3.zero;
    private float maxTravelDistance = 20f;

    public void SetUp(Damage damageToSet)
    {
        damage = damageToSet;
        startPos = transform.position;
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        CheckCollision();
        if (Vector3.Distance(transform.position, startPos) >= maxTravelDistance)
            Destroy();
    }

    void CheckCollision()
    {
        RaycastHit hit;
        float moveStep = speed * Time.deltaTime;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, moveStep))
        {
            Health health = hit.collider.gameObject.GetComponent<Health>();
            if (health != null)
                ProcessHit(health);
        }
    }

    void ProcessHit(Health health)
    {
        health.Damage(damage);
        Destroy();
    }

    void Destroy()
    {
        ReturnToPool();
    }
}
