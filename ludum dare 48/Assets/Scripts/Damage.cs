using UnityEngine;

public class Damage
{
    public GameObject Damager;
    public float Amount;
    public DamageType DamageType;

    public Damage(GameObject damager, float amount, DamageType damageType)
    {
        Damager = damager;
        Amount = amount;
        DamageType = damageType;
    }
}


public enum DamageType
{
    Fist,
    Kick,
    Bulet,
    Barrel
}