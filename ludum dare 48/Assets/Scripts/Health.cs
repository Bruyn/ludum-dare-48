using Sigtrap.Relays;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Relay<Damage> OnDamage = new Relay<Damage>();
    public Relay<Damage> OnDeath = new Relay<Damage>();
    
    [SerializeField] private float maxHealth = 3f;

    private float currentHealth = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(Damage damage)
    {
        currentHealth -= damage.Amount;
        OnDamage.Dispatch(damage);
        if (currentHealth <= 0)
            Die(damage);
    }

    private void Die(Damage damage)
    {
        OnDeath.Dispatch(damage);
    }
}
