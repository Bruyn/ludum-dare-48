using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public static ProjectileController Instance;

    [SerializeField]
    private Projectile projectilePrefab = null;
    private List<Projectile> projectiles = new List<Projectile>();

    private void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;
    }

    public Projectile PickProjectile()
    {
        foreach (var projectile in projectiles)
        {
            if (projectile.IsActive() == false)
            {
                projectile.PickFromPool();
                return projectile;
            }
        }
        Projectile prj = AddToPool();
        prj.PickFromPool();
        return prj;
    }

    private Projectile AddToPool()
    {
        Projectile projectile = Instantiate(projectilePrefab);
        projectiles.Add(projectile);
        projectile.ReturnToPool();
        return projectile;
    }
}
