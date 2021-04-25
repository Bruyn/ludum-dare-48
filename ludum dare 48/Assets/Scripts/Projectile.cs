using System;
using UnityEngine;

public class Projectile : PooledBehavior
{
    [SerializeField] private ParticleSystem wallSfx;
    [SerializeField] private float speed = 3f;

    private Damage damage;
    private Vector3 startPos = Vector3.zero;
    private float maxTravelDistance = 20f;

    private TrailRenderer _trail;

    private void Awake()
    {
        _trail = GetComponent<TrailRenderer>();
    }

    public void SetUp(Damage damageToSet)
    {
        damage = damageToSet;
        startPos = transform.position;
    }

    void Update()
    {
        CheckCollision();
        transform.position += transform.forward * speed * Time.deltaTime;
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
            else
            {
                SpawnWallEffect(hit.point);
                Destroy();
            }
        }
    }

    private void SpawnWallEffect(Vector3 position)
    {
        ParticleSystem sfxObject = Instantiate(wallSfx, position, Quaternion.identity);
        sfxObject.transform.LookAt(transform.position);
    }

    void ProcessHit(Health health)
    {
        health.Damage(damage);
        Destroy();
    }

    public override void ReturnToPool()
    {
        base.ReturnToPool();
        _trail.Clear();
    }

    void Destroy()
    {
        ReturnToPool();
    }
}
