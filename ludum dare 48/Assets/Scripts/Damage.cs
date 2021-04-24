using UnityEngine;

public class Damage
{
    public GameObject Damager;
    public float Amount;

    public Damage(GameObject damager, float amount)
    {
        Damager = damager;
        Amount = amount;
    }
}
