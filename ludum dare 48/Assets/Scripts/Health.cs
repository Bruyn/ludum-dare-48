using BehaviorDesigner.Runtime;
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
        if (IsDead())
            Die(damage);
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    
    private void Die(Damage damage)
    {
        OnDeath.Dispatch(damage);
        BehaviorTree tree = GetComponent<BehaviorTree>();
        if (tree)
        {
            tree.DisableBehavior();
        }
    }
}